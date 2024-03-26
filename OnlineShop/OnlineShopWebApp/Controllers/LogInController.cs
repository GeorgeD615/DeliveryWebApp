using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInViewModel)
        {
            return RedirectToAction("Index");
        }
    }
}
