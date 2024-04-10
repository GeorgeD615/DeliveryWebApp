using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly IUserRepository userRepository;
        public FavoritesViewComponent(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public IViewComponentResult Invoke()
        {
            var favorites = userRepository.GetFavorites(CommonData.currentUserId);
            var favoritesCount = favorites?.Count ?? 0;
            return View("Favorites", favoritesCount);
        }
    }
}
