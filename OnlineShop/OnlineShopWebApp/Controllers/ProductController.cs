using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id) => View(ProductsRepository.TryGetById(id));

        public string Page(int size, int count)
        {
            if (size <= 0)
                return "Количество товаров на странице должно быть больше нуля";

            if (count <= 0)
                return "Номер страницы должен быть больше нуля";

            int pages = ProductsRepository.GetCount() / size + 
                ((ProductsRepository.GetCount() % size) == 0 ? 0 : 1);

            if (count > pages)
                return "Указанной страницы не существует";

            var pageOfProducts = ProductsRepository.GetPageOfProducts(size, count, pages);

            return string.Join("\n\n", pageOfProducts);
        }
    }
}
