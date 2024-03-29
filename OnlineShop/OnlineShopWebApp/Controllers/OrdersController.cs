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
        private readonly IUserRepository userRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICartsRepository cartsRepository, IUserRepository userRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartsRepository = cartsRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Index(int userId)
        {
            var user = userRepository.TryGetById(userId);
            var cart = cartsRepository.TryGetByUserId(userId);
            return View(new OrderFormViewModel(user.Addresses, user.LastAddress, cart));
        }

        [HttpPost]
        public IActionResult CreateOrder(int userId, int addressId, string commentsToCourier)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            var address = userRepository.TryGetAddress(userId, addressId);
            userRepository.SetLastAddress(userId, address);
            var order = new Order(cart, address, commentsToCourier);
            ordersRepository.AddOrder(order);
            cartsRepository.ClearCart(userId);
            return View("OrderCreated", order);
        }
    }
}
