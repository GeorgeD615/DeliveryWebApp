using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            var result = ProductsInfo.Products.FirstOrDefault(p => p.Id == id);

            if (result != null)
                return $"{id}\n{result.Name}\n{result.Cost}\n{result.Description}";

            return "Товар не найден:(";
        }
    }
}
