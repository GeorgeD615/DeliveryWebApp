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
            if (logInViewModel.Password == logInViewModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            if (!ModelState.IsValid)
                return View(logInViewModel);

            return RedirectToAction("LogIn");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registrationViewModel)
        {
            if (registrationViewModel.Password == registrationViewModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            if(!ModelState.IsValid)
                return View(registrationViewModel);

            return RedirectToAction("Register");
        }
    }
}
