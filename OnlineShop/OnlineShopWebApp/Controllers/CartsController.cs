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
        private readonly UserManager<User> userManager;

        public CartsController(ICartsRepository cartsRepository, IProductsRepository productsRepository, UserManager<User> userManager)
        {
            this.cartsRepository = cartsRepository;
            this.productsRepository = productsRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            if (user == null)
                return View("Error");

            var cart = cartsRepository.TryGetByUserId(user.Id);

            return View(cart?.ToCartViewModel());
        }

        public IActionResult AddProduct(Guid productId)
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            var product = productsRepository.TryGetById(productId);

            if(product != null && user != null)
                cartsRepository.AddProduct(product, user.Id);

            return RedirectToAction(nameof(Index), new { userName = user.UserName });
        }

        public IActionResult ClearCart()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            cartsRepository.ClearCartByUserId(user.Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeProductAmount(Guid cartItemId, int difference)
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            cartsRepository.ChangeProductAmount(user.Id, cartItemId, difference);
            return RedirectToAction(nameof(Index));
        }
    }
}
