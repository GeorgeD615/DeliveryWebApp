using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(ProductsRepository.GetAll());
    }
}
