using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index(int userId)
        {
            var cart = CartsRepository.TryGetByUserId(userId);
            return View(cart);
        }

        public IActionResult AddProduct(int userId, int productId)
        {
            CartsRepository.AddProduct(ProductsRepository.TryGetById(productId), userId);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult ClearCart(int userId)
        {
            CartsRepository.ClearCart(userId);
            return RedirectToAction("Index", new { userId });
        }
    }
}
