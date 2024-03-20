using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartsRepository cartsRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICartsRepository cartsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartsRepository = cartsRepository;
        }

        public IActionResult Index(int userId)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            return View(cart);
        }

        [HttpPost]
        public IActionResult CreateOrder(int userId, string city, string street, string house, string flat, string commentsToCourier)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            var address = new Address(city, street, house, flat);
            var newOrder = new Order(cart, address, commentsToCourier);
            ordersRepository.AddOrder(newOrder);
            cartsRepository.ClearCart(userId);
            return View("OrderCreated", newOrder);
        }
    }
}
