using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Text;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            var stringBuilder = new StringBuilder(100);
            foreach (var p in ProductsRepository.Products)
            {
                stringBuilder.Append($"{p.Id}\n{p.Name}\n{p.Cost}\n\n");
            }
            return stringBuilder.ToString();
        }
    }
}
