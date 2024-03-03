using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Text;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Index()
        {
            List<Product> products = new List<Product>()
            {
                new Product {Id = 1, Name = "Ёчпочмак", Cost = 90, Description = "ќчень вкусна€ булочка"},
                new Product {Id = 2, Name = "’ачапури", Cost = 125, Description = "ќчень вкусна€ лепЄшка"},
                new Product {Id = 3, Name = "’инкали", Cost = 300, Description = "ќчень вкусные пельмени"}
            };

            StringBuilder stringBuilder = new StringBuilder(100);
            foreach(var p in products)
            {
                stringBuilder.Append($"{p.Id}\n{p.Name}\n{p.Cost}\n\n");
            }

            return stringBuilder.ToString();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
