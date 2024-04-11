using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly IUsersRepository userRepository;
        public FavoritesViewComponent(IUsersRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public IViewComponentResult Invoke()
        {
            var favorites = userRepository.TryGetFavorites(CommonData.CurrentUserId);
            var favoritesCount = favorites?.Count ?? 0;
            return View("Favorites", favoritesCount);
        }
    }
}
