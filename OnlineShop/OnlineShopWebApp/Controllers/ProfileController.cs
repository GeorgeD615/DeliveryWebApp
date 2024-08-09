using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Implementations;
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
        public IActionResult AddAddress(AddressViewModel address)
        {
            address.City = address.City.Trim();
            address.Street = address.Street.Trim();
            address.House = address.House.Trim();

            if (!ModelState.IsValid)
                return View(nameof(Index), address);

            var user = usersManager.FindByIdAsync(address.UserId).Result;
            var newAddress = address.ToAddress();
            newAddress.UserId = user.Id;

            addressRepository.Add(newAddress);

            return RedirectToAction("Index", "Orders", new { userId = address.UserId });
        }

        [HttpPost]
        public IActionResult RemoveAddress(Guid userId, Guid addressId)
        {
            var user = usersManager.FindByIdAsync(userId.ToString()).Result;

            var address = user!.Addresses.FirstOrDefault(address => address.Id == addressId);

            if (address == null)
                return View("Error");

            bool isLast = address.IsLast;

            user.Addresses.Remove(address);

            if (isLast && user.Addresses.Any())
                user.Addresses[0].IsLast = true;

            usersManager.UpdateAsync(user);
            return RedirectToAction("Index", "Orders", new { userId });
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var user = usersManager.GetUserAsync(HttpContext.User).Result;

            if (user == null) 
                return View("Error");

            var addresses = addressRepository.GetByUserId(user.Id);
            var orders = ordersRepository.GetByUserId(user.Id);
            var avatar = imagesRepository.TryGetAvatarByUserId(user.Id);

            var profile = new ProfileViewModel()
            {
                Login = user.UserName,
                Addresses = addresses.Select(ModelConverter.ToAddressViewModel).ToList(),
                Orders = orders.Select(order => order.ToOrderViewModel()).ToList(),
                AvatarImagesPath = avatar == null ? Constants.DefaultUserImagePath : avatar.Url
            };

            return View(profile);
        }

        public IActionResult OrderInfo(Guid orderId)
        {
            var order = ordersRepository.TryGetById(orderId);
            return View(order?.ToOrderViewModel());
        }

        [HttpPost]
        public IActionResult Profile(ProfileViewModel profileModel)
        {
            profileModel.Login = profileModel.Login.Trim();

            var currentUser = usersManager.GetUserAsync(HttpContext.User).Result;

            var user = usersManager.FindByNameAsync(profileModel.Login).Result;

            if (user != null && user.Id != currentUser.Id)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
            {
                var addresses = addressRepository.GetByUserId(currentUser.Id);
                var orders = ordersRepository.GetByUserId(currentUser.Id);

                profileModel.Addresses = addresses.Select(ModelConverter.ToAddressViewModel).ToList();
                profileModel.Orders = orders.Select(order => order.ToOrderViewModel()).ToList();
                return View(profileModel);
            }

            if(profileModel.UploadedFile != null)
            {
                var uploadedImagePath = imagesProvider.SafeFile(profileModel.UploadedFile, ImageFolder.avatars);
                imagesRepository.SetAvatar(uploadedImagePath.ToAvatar(currentUser.Id));
            }

            currentUser.UserName = profileModel.Login;

            usersManager.UpdateAsync(currentUser).Wait();

            return RedirectToAction(nameof(Profile));
        }
    }
}