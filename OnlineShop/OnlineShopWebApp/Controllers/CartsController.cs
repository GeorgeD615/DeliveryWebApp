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

        public async Task<ActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return View("Error");

            var cart = await cartsRepository.TryGetByUserIdAsync(user.Id);

            return View(cart?.ToCartViewModel());
        }

        public async Task<ActionResult> AddProductAsync(Guid productId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var product = await productsRepository.TryGetByIdAsync(productId);

            if(product != null && user != null)
                await cartsRepository.AddProductAsync(product, user.Id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> ClearCartAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await cartsRepository.ClearCartByUserIdAsync(user.Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> ChangeProductAmountAsync(Guid cartItemId, int difference)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await cartsRepository.ChangeProductAmountAsync(user.Id, cartItemId, difference);
            return RedirectToAction(nameof(Index));
        }
    }
}
