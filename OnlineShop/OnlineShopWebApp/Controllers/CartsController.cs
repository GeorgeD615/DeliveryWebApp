using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IProductsRepository productsRepository;
        private readonly UserManager<User> usersManager;

        public CartsController(ICartsRepository cartsRepository, IProductsRepository productsRepository, UserManager<User> userManager)
        {
            this.cartsRepository = cartsRepository;
            this.productsRepository = productsRepository;
            this.usersManager = userManager;
        }

        public IActionResult Index(string userName)
        {
            var user = usersManager.FindByNameAsync(userName).Result;

            if (user == null)
                return View("Error");

            var cart = cartsRepository.TryGetByUserId(user.Id);

            return View(cart?.ToCartViewModel());
        }

        public IActionResult AddProduct(string userName, Guid productId)
        {
            var user = usersManager.FindByNameAsync(userName).Result;

            var product = productsRepository.TryGetById(productId);

            if(product != null && user != null)
                cartsRepository.AddProduct(product, user.Id);

            return RedirectToAction(nameof(Index), new { userName = user.UserName });
        }

        public IActionResult ClearCart(string userId)
        {
            var user = usersManager.FindByIdAsync(userId).Result;
            cartsRepository.ClearCartByUserId(userId);
            return RedirectToAction(nameof(Index), new { userName = user.UserName });
        }

        public IActionResult ChangeProductAmount(string userId, Guid cartItemId, int difference)
        {
            var user = usersManager.FindByIdAsync(userId).Result;
            cartsRepository.ChangeProductAmount(user.Id, cartItemId, difference);
            return RedirectToAction(nameof(Index), new { userName = user.UserName });
        }
    }
}
