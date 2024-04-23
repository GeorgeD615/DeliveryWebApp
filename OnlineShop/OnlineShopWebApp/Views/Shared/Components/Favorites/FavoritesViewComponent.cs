using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models;

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
