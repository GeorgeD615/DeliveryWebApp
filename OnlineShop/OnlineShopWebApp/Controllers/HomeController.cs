using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;

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
            var searchName = name?.Trim();
            if (string.IsNullOrEmpty(searchName))
                return View(productsRepository.GetAll());
            return View(productsRepository.SearchByName(searchName).Select(ModelConverter.ConvertToProductViewModel));
        }
    }
}
