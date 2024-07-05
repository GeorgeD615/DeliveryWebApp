﻿using Microsoft.AspNetCore.Identity;
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

        public IViewComponentResult Invoke(bool isAuthenticated, string userName)
        {
            if(isAuthenticated)
            {
                var user = userManager.FindByNameAsync(userName).Result;
                var favoritesCount = favoritesRepository.GetByUserId(user.Id)?.Count() ?? 0;
                return View("Favorites", favoritesCount);
            }

            return View("Favorites", 0);
        }
    }
}