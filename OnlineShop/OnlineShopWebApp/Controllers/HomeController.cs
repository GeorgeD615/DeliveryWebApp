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
            foreach (var product in ProductsRepository.GetAll())
            {
                stringBuilder.Append($"{product}\n\n");
            }
            return stringBuilder.ToString();
        }
    }
}
