using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registrationViewModel)
        {
            return RedirectToAction("Index");
        }
    }
}
