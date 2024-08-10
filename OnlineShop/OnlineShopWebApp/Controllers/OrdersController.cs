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

        public async Task<ActionResult> Index(string userId)
        {
            var user = await usersManager.FindByIdAsync(userId);

            if (user == null)
                return RedirectToAction("Index", "Carts");

            var cart = await cartsRepository.TryGetByUserIdAsync(user.Id);

            if (cart == null)
                return RedirectToAction("Index", "Carts");

            var addresses = await addressesRepository.GetByUserIdAsync(user.Id);

            return View(new OrderFormViewModel(
                addresses.Select(address => address.ToAddressViewModel()).ToList(),
                addresses.FirstOrDefault(a => a.IsLast)?.ToAddressViewModel(),
                cart.ToCartViewModel(),
                userId) 
            );
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(string userId, Guid addressId, string commentsToCourier)
        {
            var user = await usersManager.FindByIdAsync(userId.ToString());
            var cart = await cartsRepository.TryGetByUserIdAsync(userId);
            await addressesRepository.ResetLastAddressAsync(userId, addressId);

            var address = await addressesRepository.TryGetByIdAsync(addressId);

            var order = new Order()
            {
                CartItems = cart.Items,
                Address = address,
                CommentsToCourier = commentsToCourier?.Trim(),
                UserId = user.Id,
                StateOfOrder = StateOfOrder.InProcessing,
                TimeOfOrder = DateTime.Now
            };
            await ordersRepository.AddAsync(order);
            await cartsRepository.ClearCartByUserIdAsync(userId);
            return View("OrderCreated", order.ToOrderViewModel());
        }
    }
}