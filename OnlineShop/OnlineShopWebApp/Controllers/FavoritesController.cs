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

        public IActionResult Index()
        {
            var user = usersManager.GetUserAsync(HttpContext.User).Result;
            var favorites = favoritesRepository.GetByUserId(user.Id);
            return View(favorites?.Select(favorite => favorite.ToProductViewModel()).ToList());
        }

        public IActionResult AddFavorite(Guid productId)
        {
            var favoriteProduct = productsRepository.TryGetById(productId);

            var user = usersManager.GetUserAsync(HttpContext.User).Result;

            if (favoriteProduct != null)
                favoritesRepository.Add(user, favoriteProduct);

            return RedirectToAction(nameof(Index), new { userName = user.UserName });
        }

        public IActionResult RemoveFavorite(Guid productId)
        {
            var user = usersManager.GetUserAsync(HttpContext.User).Result;
            var product = favoritesRepository.GetByUserId(user.Id).FirstOrDefault(p => p.Id == productId);

            if (user != null && product != null)
                favoritesRepository.Remove(user, product);

            return RedirectToAction(nameof(Index), new { user.UserName });
        }
    }
}