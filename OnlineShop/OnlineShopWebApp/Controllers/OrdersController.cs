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

        public IActionResult CreateOrder(int userId)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            ordersRepository.AddOrder(new Order(string.Empty, string.Empty, cart, string.Empty));
            cartsRepository.ClearCart(userId);
            return View("OrderCreated");
        }
    }
}
