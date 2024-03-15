using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index(int userId)
        {
            var cart = CartsRepository.GetByUserId(userId);
            return View(cart);
        }

        public IActionResult AddProduct(int userId, int productId)
        {
            var product = ProductsRepository.TryGetById(productId);
            CartsRepository.AddProduct(product, userId);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult ClearCart(int userId)
        {
            CartsRepository.ClearCart(userId);
            return RedirectToAction("Index", new { userId });
        }
    }
}
