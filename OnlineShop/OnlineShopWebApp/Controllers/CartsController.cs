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

        public IActionResult Index(Guid userId)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            return View(cart);
        }

        public IActionResult AddProduct(Guid userId, Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            cartsRepository.AddProduct(product, userId);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult ClearCart(Guid userId)
        {
            cartsRepository.ClearCart(userId);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult ChangeProductAmount(Guid userId, Guid cartItemId, int difference)
        {
            cartsRepository.ChangeProductAmount(userId, cartItemId, difference);
            return RedirectToAction("Index", new { userId });
        }
    }
}
