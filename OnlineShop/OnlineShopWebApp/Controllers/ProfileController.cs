using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepository userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Index() => View(); 

        [HttpPost]
        public IActionResult AddAddress(int userId, Address address)
        {
            address.City = address.City.Trim();
            address.Street = address.Street.Trim();
            address.House = address.House.Trim();

            if (!ModelState.IsValid)
                return View("Index", address);
            userRepository.AddAddress(userId, address);
            return RedirectToAction("Index", "Orders", new { userId });
        }

        [HttpPost]
        public IActionResult RemoveAddress(int userId, int addressId)
        {
            userRepository.RemoveAddress(userId, addressId);
            return RedirectToAction("Index", "Orders", new { userId });
        }
    }
}
