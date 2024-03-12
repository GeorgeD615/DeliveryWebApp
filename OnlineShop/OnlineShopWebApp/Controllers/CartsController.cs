using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Products;
using System.Globalization;

namespace OnlineShopWebApp.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index(int userId)
        {
            var cart = CartsRepository.TryGetByUserId(userId);
            ViewData["Cost"] = cart?.Cost.ToString("#,#", new CultureInfo("ru-RU"));
            return View(cart?.GetAll());
        }

        public IActionResult AddProductToCart(int userId, int productId)
        {
            var cart = CartsRepository.AddProductToCartAndGetCart(ProductsRepository.TryGetById(productId), userId);
            ViewData["Cost"] = cart?.Cost.ToString("#,#", new CultureInfo("ru-RU"));
            return View("Index", cart?.GetAll());
        }
    }
}
