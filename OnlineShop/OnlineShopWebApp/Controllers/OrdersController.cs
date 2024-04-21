using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.ContainersForView;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IUsersRepository usersRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICartsRepository cartsRepository, IUsersRepository userRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartsRepository = cartsRepository;
            this.usersRepository = userRepository;
        }

        public IActionResult Index(Guid userId)
        {
            var user = usersRepository.TryGetById(userId);
            var cart = cartsRepository.TryGetByUserId(userId);

            if (user == null || cart == null)
                return RedirectToAction("Index", "Carts");

            return View(new OrderFormViewModel(user.Addresses, user.LastAddress, ModelConverter.ConvertToCartViewModel(cart)));
        }

        [HttpPost]
        public IActionResult CreateOrder(Guid userId, Guid addressId, string commentsToCourier)
        {
            var cart = cartsRepository.TryGetByUserId(userId);
            var address = usersRepository.TryGetAddress(userId, addressId);
            usersRepository.SetLastAddress(userId, address);
            var user = usersRepository.TryGetById(userId);
            var order = new Order(cart, address, commentsToCourier.Trim(), user);
            ordersRepository.Add(order);
            cartsRepository.ClearCart(userId);
            return View("OrderCreated", order);
        }
    }
}
