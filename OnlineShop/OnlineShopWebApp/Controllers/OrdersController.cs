using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Enums;
using OnlineShopWebApp.Models.ContainersForView;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IAddressesRepository addressesRepository;
        private readonly UserManager<User> usersManager;

        public OrdersController(IOrdersRepository ordersRepository, ICartsRepository cartsRepository, UserManager<User> usersManager, IAddressesRepository addressRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartsRepository = cartsRepository;
            this.usersManager = usersManager;
            this.addressesRepository = addressRepository;
        }

        public IActionResult Index(string userId)
        {
            var user = usersManager.FindByIdAsync(userId).Result;

            if (user == null)
                return RedirectToAction("Index", "Carts");

            var cart = cartsRepository.TryGetByUserId(user.Id);

            if (cart == null)
                return RedirectToAction("Index", "Carts");

            var addresses = addressesRepository.GetByUserId(user.Id);

            return View(new OrderFormViewModel(
                addresses.Select(address => address.ToAddressViewModel()).ToList(),
                addresses.FirstOrDefault(a => a.IsLast)?.ToAddressViewModel(),
                cart.ToCartViewModel(),
                userId)
            );
        }

        [HttpPost]
        public IActionResult CreateOrder(string userId, Guid addressId, string commentsToCourier)
        {
            var user = usersManager.FindByIdAsync(userId.ToString()).Result;
            var cart = cartsRepository.TryGetByUserId(userId);
            addressesRepository.ResetLastAddress(userId, addressId);

            var address = addressesRepository.TryGetById(addressId);

            var order = new Order()
            {
                CartItems = cart.Items,
                Address = address,
                CommentsToCourier = commentsToCourier?.Trim(),
                UserId = user.Id,
                StateOfOrder = StateOfOrder.InProcessing,
                TimeOfOrder = DateTime.Now
            };
            ordersRepository.Add(order);
            cartsRepository.ClearCartByUserId(userId);
            return View("OrderCreated", order.ToOrderViewModel());
        }
    }
}