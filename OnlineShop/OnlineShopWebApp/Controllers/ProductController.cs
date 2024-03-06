using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Text;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            var result = ProductsRepository.GetProductById(id);

            if (result != null)
                return $"{id}\n{result.Name}\n{result.Cost}\n{result.Description}";

            return "Товар не найден:(";
        }

        public string Page(int productsNum, int pageNum)
        {
            if (productsNum <= 0)
                return "Количество товаров на странице должно быть больше нуля";

            int pages = ProductsRepository.Products.Count / productsNum + 
                ((ProductsRepository.Products.Count % productsNum) == 0 ? 0 : 1);

            if (pageNum > pages)
                return "Указанной страницы не существует";

            var pageOfProducts = ProductsRepository.GetPageOfProducts(productsNum, pageNum, pages);

            var stringBuilder = new StringBuilder(100);
            foreach (var p in pageOfProducts)
            {
                stringBuilder.Append($"{p.Id}\n{p.Name}\n{p.Cost}\n\n");
            }

            return stringBuilder.ToString();
        }
    }
}
