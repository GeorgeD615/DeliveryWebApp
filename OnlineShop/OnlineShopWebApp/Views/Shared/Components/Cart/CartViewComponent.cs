﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;
        private readonly UserManager<User> usersManager;

        public CartViewComponent(ICartsRepository cartsRepository, UserManager<User> usersManager)
        {
            this.cartsRepository = cartsRepository;
            this.usersManager = usersManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                var user = await usersManager.GetUserAsync(HttpContext.User);
                var cart = await cartsRepository.TryGetByUserIdAsync(user.Id);
                var amount = cart?.Items.Sum(item => item.Amount) ?? 0;
                return View("Cart", amount);
            }
            return View("Cart", 0);
        }
    }
}