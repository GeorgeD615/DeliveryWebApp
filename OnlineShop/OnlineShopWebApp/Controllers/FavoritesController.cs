using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IFavoritesRepository favoritesRepository;
        private readonly UserManager<User> usersManager;

        public FavoritesController(IProductsRepository productsRepository, UserManager<User> usersManager, IFavoritesRepository favoritesRepository)
        {
            this.productsRepository = productsRepository;
            this.usersManager = usersManager;
            this.favoritesRepository = favoritesRepository;
        }

        public async Task<ActionResult> Index()
        {
            var user = usersManager.GetUserAsync(HttpContext.User).Result;
            var favorites = await favoritesRepository.GetByUserIdAsync(user.Id);
            return View(favorites?.Select(favorite => favorite.ToProductViewModel()).ToList());
        }

        public async Task<ActionResult> AddFavoriteAsync(Guid productId)
        {
            var favoriteProduct = await productsRepository.TryGetByIdAsync(productId);

            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (favoriteProduct != null)
                await favoritesRepository.AddAsync(user, favoriteProduct);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> RemoveFavoriteAsync(Guid productId)
        {
            var user = usersManager.GetUserAsync(HttpContext.User).Result;
            var product = (await favoritesRepository.GetByUserIdAsync(user.Id)).FirstOrDefault(p => p.Id == productId);

            if (user != null && product != null)
                await favoritesRepository.RemoveAsync(user, product);

            return RedirectToAction(nameof(Index));
        }
    }
}