using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Users;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> usersManager;
        private readonly IAddressesRepository addressRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IImagesRepository imagesRepository;
        private readonly ImagesProvider imagesProvider;

        public ProfileController(UserManager<User> usersManager, IAddressesRepository addressRepository, 
            IOrdersRepository ordersRepository, ImagesProvider imagesProvider, IImagesRepository imagesRepository)
        {
            this.usersManager = usersManager;
            this.addressRepository = addressRepository;
            this.ordersRepository = ordersRepository;
            this.imagesProvider = imagesProvider;
            this.imagesRepository = imagesRepository;
        }

        public IActionResult Index(string userId) => View(new AddressViewModel() { UserId = userId });
         
        [HttpPost]
        public async Task<ActionResult> AddAddressAsync(AddressViewModel address)
        {
            address.City = address.City.Trim();
            address.Street = address.Street.Trim();
            address.House = address.House.Trim();

            if (!ModelState.IsValid)
                return View(nameof(Index), address);

            var user = usersManager.FindByIdAsync(address.UserId).Result;
            var newAddress = address.ToAddress();
            newAddress.UserId = user.Id;

            await addressRepository.AddAsync(newAddress);

            return RedirectToAction("Index", "Orders", new { userId = address.UserId });
        }

        [HttpPost]
        public async Task<ActionResult> RemoveAddress(Guid addressId)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var address = user!.Addresses.FirstOrDefault(address => address.Id == addressId);

            if (address == null)
                return View("Error");

            bool isLast = address.IsLast;

            user.Addresses.Remove(address);

            if (isLast && user.Addresses.Any())
                user.Addresses[0].IsLast = true;

            await usersManager.UpdateAsync(user);
            return RedirectToAction("Index", "Orders", new { user.Id });
        }

        [HttpGet]
        public async Task<ActionResult> ProfileAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (user == null) 
                return View("Error");

            var addresses = await addressRepository.GetByUserIdAsync(user.Id);
            var orders = await ordersRepository.GetByUserIdAsync(user.Id);
            var avatar = await imagesRepository.TryGetAvatarByUserIdAsync(user.Id);

            var profile = new ProfileViewModel()
            {
                Login = user.UserName,
                Addresses = addresses.Select(ModelConverter.ToAddressViewModel).ToList(),
                Orders = orders.Select(order => order.ToOrderViewModel()).ToList(),
                AvatarImagesPath = avatar == null ? Constants.DefaultUserImagePath : avatar.Url
            };

            return View(profile);
        }

        public async Task<ActionResult> OrderInfoAsync(Guid orderId)
        {
            var order = await ordersRepository.TryGetByIdAsync(orderId);
            return View(order?.ToOrderViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> ProfileAsync(ProfileViewModel profileModel)
        {
            profileModel.Login = profileModel.Login.Trim();

            var currentUser = await usersManager.GetUserAsync(HttpContext.User);

            var user = await usersManager.FindByNameAsync(profileModel.Login);

            if (user != null && user.Id != currentUser.Id)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
            {
                var addresses = await addressRepository.GetByUserIdAsync(currentUser.Id);
                var orders = await ordersRepository.GetByUserIdAsync(currentUser.Id);

                profileModel.Addresses = addresses.Select(ModelConverter.ToAddressViewModel).ToList();
                profileModel.Orders = orders.Select(order => order.ToOrderViewModel()).ToList();
                return View(profileModel);
            }

            if(profileModel.UploadedFile != null)
            {
                var uploadedImagePath = imagesProvider.SafeFile(profileModel.UploadedFile, ImageFolder.avatars);
                await imagesRepository.SetAvatarAsync(uploadedImagePath.ToAvatar(currentUser.Id));
            }

            currentUser.UserName = profileModel.Login;

            await usersManager.UpdateAsync(currentUser);

            return RedirectToAction("Profile");
        }
    }
}