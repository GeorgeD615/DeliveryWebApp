using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index(int userId) => View(CartsRepository.TryGetByUserId(userId));

        public IActionResult AddProductToCart(int userId, int productId)
        {
            CartsRepository.AddProductToUserCart(ProductsRepository.TryGetById(productId), userId);
            return View("Index", CartsRepository.TryGetByUserId(userId));
        }
    }
}
