using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly IFavoritesRepository favoritesRepository;
        private readonly UserManager<User> usersManager;

        public FavoritesViewComponent(UserManager<User> userManager, IFavoritesRepository favoritesRepository)
        {
            this.usersManager = userManager;
            this.favoritesRepository = favoritesRepository;
        }

        public IViewComponentResult Invoke(bool isAuthenticated, string userName)
        {
            if(isAuthenticated)
            {
                var user = usersManager.FindByNameAsync(userName).Result;
                var favoritesCount = favoritesRepository.GetByUserId(user.Id)?.Count() ?? 0;
                return View("Favorites", favoritesCount);
            }

            return View("Favorites", 0);
        }
    }
}