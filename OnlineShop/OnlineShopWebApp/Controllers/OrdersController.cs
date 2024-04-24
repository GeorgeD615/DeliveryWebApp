﻿using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Enums;
using OnlineShopWebApp.Models.ContainersForView;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Orders;

namespace OnlineShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IUsersRepository usersRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICartsRepository cartsRepository, IUsersRepository usersRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartsRepository = cartsRepository;
            this.usersRepository = usersRepository;
        }

        public IActionResult Index(Guid userId)
        {
            var user = usersRepository.TryGetById(userId);
            var cart = cartsRepository.TryGetNotYetOrderedByUserId(userId);

            if (user == null || cart == null)
                return RedirectToAction("Index", "Carts");

            return View(new OrderFormViewModel(
                user.Addresses.Select(ModelConverter.ConvertToAddressViewModel).ToList(),
                ModelConverter.ConvertToAddressViewModel(user.Addresses.FirstOrDefault(a => a.IsLast)),
                ModelConverter.ConvertToCartViewModel(cart))
            );
        }

        [HttpPost]
        public IActionResult CreateOrder(Guid userId, Guid addressId, string commentsToCourier)
        {
            var cart = cartsRepository.TryGetNotYetOrderedByUserId(userId);
            var address = usersRepository.TryGetAddress(userId, addressId);
            usersRepository.SetLastAddress(userId, addressId);
            var user = usersRepository.TryGetById(userId);
            var order = new Order()
            {
                Cart = cart,
                Address = address,
                CommentsToCourier = commentsToCourier.Trim(),
                User = user,
                StateOfOrder = StateOfOrder.InProcessing,
                TimeOfOrder = DateTime.Now
            };
            ordersRepository.Add(order);
            cartsRepository.ClearCart(userId);
            return View("OrderCreated", ModelConverter.ConvertToOrderViewModel(order));
        }
    }
}
