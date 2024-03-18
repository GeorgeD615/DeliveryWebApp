using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IProductsRepository productsRepository;

        public CartsController(ICartsRepository cartsRepository, IProductsRepository productsRepository)
        {
            this.cartsRepository = cartsRepository;
            this.productsRepository = productsRepository;
        }

        public IActionResult Index(int userId)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            return View(cart);
        }

        public IActionResult AddProduct(int userId, int productId)
        {
            var product = productsRepository.TryGetById(productId);
            cartsRepository.AddProduct(product, userId);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult ClearCart(int userId)
        {
            cartsRepository.ClearCart(userId);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult ChangeProductAmount(int userId, string cartItemId, int difference)
        {
            cartsRepository.ChangeProductAmount(userId, new Guid(cartItemId), difference);
            return RedirectToAction("Index", new { userId });
        }
    }
}
