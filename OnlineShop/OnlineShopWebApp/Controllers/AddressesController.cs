using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IUserRepository userRepository;

        public AddressesController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Index() => View(); 

        [HttpPost]
        public IActionResult AddAddress(int userId, string city, string street, string house, string flat)
        {
            var address = new Address(userId, city, street, house, flat);
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
