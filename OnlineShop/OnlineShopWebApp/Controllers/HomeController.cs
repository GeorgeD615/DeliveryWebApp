using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        public string Index() => string.Join("\n\n", ProductsRepository.GetAll());
    }
}
