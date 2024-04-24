using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;

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
            var cart = cartsRepository.TryGetNotYetOrderedByUserId(userId);
            return View(ModelConverter.ConvertToCartViewModel(cart));
        }

        public IActionResult AddProduct(Guid userId, Guid productId)
        {
            var product = productsRepository.TryGetById(productId);

            if(product != null)
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
