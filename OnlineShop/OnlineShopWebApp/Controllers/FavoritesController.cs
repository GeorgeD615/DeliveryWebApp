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

        public IActionResult Index(Guid userId)
        {
            var favorites = userRepository.GetFavorites(userId);
            return View(favorites);
        }

        public IActionResult AddFavorite(Guid userId, Guid productId) 
        { 
            var favoriteProduct = productsRepository.TryGetById(productId);
            userRepository.AddFavorite(userId, favoriteProduct);
            return RedirectToAction("Index", new { userId });
        }

        public IActionResult RemoveFavorite(Guid userId, Guid productId)
        {
            userRepository.RemoveFavoriteById(userId, productId);
            return RedirectToAction("Index", new { userId });
        }
    }
}
