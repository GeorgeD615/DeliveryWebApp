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

        public async Task<ActionResult> Index()
        {
            var orders = await ordersRepository.GetAllAsync();
            return View(orders?.Select(order => order.ToOrderViewModel()).ToList());
        }
        public async Task<ActionResult> ShowInfoAsync(Guid orderId)
        {
            var order = await ordersRepository.TryGetByIdAsync(orderId);
            return View(order?.ToOrderViewModel());
        }
        public async Task<ActionResult> EditStatusAsync(Guid orderId, StateOfOrder status)
        {
            await ordersRepository.EditStatusAsync(orderId, status);
            return RedirectToAction("ShowInfo", new { orderId });
        }
    }
}
