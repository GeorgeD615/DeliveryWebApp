using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUsersRepository userRepository;

        public ProfileController(IUsersRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Index() => View(); 

        [HttpPost]
        public IActionResult AddAddress(Guid userId, Address address)
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
        public IActionResult RemoveAddress(Guid userId, Guid addressId)
        {
            userRepository.RemoveAddress(userId, addressId);
            return RedirectToAction("Index", "Orders", new { userId });
        }
    }
}
