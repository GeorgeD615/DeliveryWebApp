using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUsersRepository usersRepository;

        public ProfileController(IUsersRepository userRepository)
        {
            this.usersRepository = userRepository;
        }

        public IActionResult Index() => View(); 

        [HttpPost]
        public IActionResult AddAddress(Guid userId, AddressViewModel address)
        {
            address.City = address.City.Trim();
            address.Street = address.Street.Trim();
            address.House = address.House.Trim();

            if (!ModelState.IsValid)
                return View("Index", address);
            usersRepository.AddAddress(userId, ModelConverter.ConvertToAddress(address));
            return RedirectToAction("Index", "Orders", new { userId });
        }

        [HttpPost]
        public IActionResult RemoveAddress(Guid userId, Guid addressId)
        {
            usersRepository.RemoveAddress(userId, addressId);
            return RedirectToAction("Index", "Orders", new { userId });
        }
    }
}
