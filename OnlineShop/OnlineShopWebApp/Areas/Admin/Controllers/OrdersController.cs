using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Enums;
using OnlineShopWebApp.Models.Helpers;
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
            return View(orders?.Select(ModelConverter.ConvertToOrderViewModel).ToList());
        }
        public IActionResult ShowInfo(Guid orderId)
        {
            var order = ordersRepository.TryGetById(orderId);
            return View(ModelConverter.ConvertToOrderViewModel(order));
        }
        public IActionResult EditStatus(Guid orderId, StateOfOrder status)
        {
            ordersRepository.EditStatus(orderId, status);
            return RedirectToAction("ShowInfo", new { orderId });
        }
    }
}
