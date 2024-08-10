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

        public async Task<ActionResult> Index(string name) {
            var searchName = name?.Trim();

            if (string.IsNullOrEmpty(searchName))
                return View((await productsRepository.GetAllAsync())
                    .Select(product => product.ToProductViewModel())
                    .ToList());

            var products = await productsRepository.SearchByNameAsync(searchName);

            return View(products
                .Select(product => product.ToProductViewModel())
                .ToList());
        }
    }
}
