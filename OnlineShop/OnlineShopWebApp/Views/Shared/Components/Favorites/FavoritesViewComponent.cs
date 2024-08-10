using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly IFavoritesRepository favoritesRepository;
        private readonly UserManager<User> userManager;

        public FavoritesViewComponent(UserManager<User> userManager, IFavoritesRepository favoritesRepository)
        {
            this.userManager = userManager;
            this.favoritesRepository = favoritesRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isAuthenticated)
        {
            if(isAuthenticated)
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                var favoritesCount = (await favoritesRepository.GetByUserIdAsync(user.Id))?.Count() ?? 0;
                return View("Favorites", favoritesCount);
            }

            return View("Favorites", 0);
        }
    }
}