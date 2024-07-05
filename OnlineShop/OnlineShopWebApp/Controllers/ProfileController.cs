using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> usersManager;
        private readonly IAddressRepository addressRepository;

        public ProfileController(UserManager<User> usersManager, IAddressRepository addressRepository)
        {
            this.usersManager = usersManager;
            this.addressRepository = addressRepository;
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
                throw new Exception("Адрес не найден");

            bool isLast = address.IsLast;

            user.Addresses.Remove(address);

            if (isLast && user.Addresses.Any())
                user.Addresses[0].IsLast = true;

            usersManager.UpdateAsync(user);
            return RedirectToAction("Index", "Orders", new { userId });
        }
    }
}