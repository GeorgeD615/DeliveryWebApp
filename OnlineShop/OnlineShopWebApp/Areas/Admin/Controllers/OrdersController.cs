using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Orders;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            return View(orders);
        }
        public IActionResult ShowInfo(int orderId)
        {
            var order = ordersRepository.TryGetOrder(orderId);
            return View(order);
        }
        public IActionResult EditStatus(int orderId, StateOfOrder status)
        {
            ordersRepository.EditStatus(orderId, status);
            return RedirectToAction("ShowInfo", new { orderId });
        }
    }
}
