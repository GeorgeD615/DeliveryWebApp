using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult LogIn() => View();

        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInViewModel)
        {
            return RedirectToAction("LogIn");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registrationViewModel)
        {
            return RedirectToAction("Register");
        }
    }
}
