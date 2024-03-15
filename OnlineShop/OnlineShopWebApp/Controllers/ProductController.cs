using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id) => View(ProductsRepository.TryGetById(id));

        public IActionResult Page(int size, int count)
        {
            if (size <= 0)
                return View(null);

            if (count <= 0)
                return View(null);

            int pages = ProductsRepository.GetCount() / size + 
                ((ProductsRepository.GetCount() % size) == 0 ? 0 : 1);

            if (count > pages)
                return View(null);

            ViewData["pages"] = pages;
            ViewData["size"] = size;
            switch (size)
            {
                case 3:
                    ViewData["cardSize"] = 3;
                    ViewData["imageSize"] = 200;
                    break;
                case 5:
                case 10:
                    ViewData["imageSize"] = 125;
                    ViewData["cardSize"] = 2;
                    break;
            } 

            var pageOfProducts = ProductsRepository.GetPageOfProducts(size, count, pages);

            return View(pageOfProducts);
        }
    }
}
