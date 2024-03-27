using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public HomeController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index(string name) {
            if (name == null)
                return View(productsRepository.GetAll());
            return View(productsRepository.GetByName(name));
        }
    }
}
