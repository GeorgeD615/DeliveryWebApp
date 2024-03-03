using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Text;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            List<Product> products = new List<Product>()
            {
                new Product {Id = 1, Name = "Эчпочмак", Cost = 90, Description = "Очень вкусная булочка"},
                new Product {Id = 2, Name = "Хачапури", Cost = 125, Description = "Очень вкусная лепёшка"},
                new Product {Id = 3, Name = "Хинкали", Cost = 300, Description = "Очень вкусные пельмени"}
            };

            Product result = products.FirstOrDefault(x => x.Id == id);

            if (result != null)
                return $"{result.Id}\n{result.Name}\n{result.Cost}\n{result.Description}";
            else
                return "Товар не найден:(";
        }
    }
}
