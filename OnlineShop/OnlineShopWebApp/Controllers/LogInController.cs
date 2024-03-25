using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult LogIn(string login, string password, string rememberMe)
        {
            return RedirectToAction("Index");
        }
    }
}
