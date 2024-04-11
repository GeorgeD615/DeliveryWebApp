using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.ContainersForView;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IUsersRepository userRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICartsRepository cartsRepository, IUsersRepository userRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartsRepository = cartsRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Index(Guid userId)
        {
            var user = userRepository.TryGetById(userId);
            var cart = cartsRepository.TryGetByUserId(userId);

            if (user == null || cart == null)
                return RedirectToAction("Index", "Carts");

            return View(new OrderFormViewModel(user.Addresses, user.LastAddress, cart));
        }

        [HttpPost]
        public IActionResult CreateOrder(Guid userId, Guid addressId, string commentsToCourier)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            var address = userRepository.TryGetAddress(userId, addressId);
            userRepository.SetLastAddress(userId, address);
            var user = userRepository.TryGetById(userId);
            var order = new Order(cart, address, commentsToCourier.Trim(), user);
            ordersRepository.Add(order);
            cartsRepository.ClearCart(userId);
            return View("OrderCreated", order);
        }
    }
}
