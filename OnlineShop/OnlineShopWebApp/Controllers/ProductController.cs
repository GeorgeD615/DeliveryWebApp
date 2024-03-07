using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Text;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            var result = ProductsRepository.TryGetById(id);

            return result != null ? $"{result}\n{result.Description}" : "Товар не найден:(";
        }

        public string Page(int productsNum, int pageNum)
        {
            if (productsNum <= 0)
                return "Количество товаров на странице должно быть больше нуля";

            int pages = ProductsRepository.GetCount() / productsNum + 
                ((ProductsRepository.GetCount() % productsNum) == 0 ? 0 : 1);

            if (pageNum > pages)
                return "Указанной страницы не существует";

            var pageOfProducts = ProductsRepository.GetPageOfProducts(productsNum, pageNum, pages);

            var stringBuilder = new StringBuilder(100);
            foreach (var product in pageOfProducts)
            {
                stringBuilder.Append($"{product}\n\n");
            }

            return stringBuilder.ToString();
        }
    }
}
