using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IUserRepository userRepository;

        public FavoritesController(IProductsRepository productsRepository, IUserRepository userRepository)
        {
            this.productsRepository = productsRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Index(int userId)
        {
            var favorites = userRepository.GetFavorites(userId);
            return View(favorites);
        }

        public IActionResult AddFavorite(int userId, int productId) 
        { 
            var favoriteProduct = productsRepository.TryGetById(productId);
            userRepository.AddFavorite(userId, favoriteProduct);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult RemoveFavorite(int userId, int productId)
        {
            var favoriteProduct = productsRepository.TryGetById(productId);
            userRepository.RemoveFavorite(userId, favoriteProduct);
            return RedirectToAction("Index", new { userId });
        }
    }
}
