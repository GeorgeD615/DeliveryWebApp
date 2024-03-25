using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Register(string login, string password, string passwordConfirm)
        {
            return RedirectToAction("Index");
        }
    }
}
