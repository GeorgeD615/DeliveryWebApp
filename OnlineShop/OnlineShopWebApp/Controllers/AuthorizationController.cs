using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> usersManager;
        private readonly SignInManager<User> signInManager;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.usersManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult LogIn(string returnUrl) =>  View(new LogInViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInViewModel)
        {
            logInViewModel.Login = logInViewModel.Login.Trim();
            logInViewModel.Password = logInViewModel.Password.Trim();

            if (!ModelState.IsValid)
                return View(logInViewModel);

            if (logInViewModel.Password == logInViewModel.Login)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
                return View(logInViewModel);
            }

            var result = signInManager.PasswordSignInAsync(logInViewModel.Login,
                logInViewModel.Password,
                logInViewModel.RememberMe,
                false).Result;

            if (result.Succeeded)
            {
                if(logInViewModel.ReturnUrl == null)
                    return RedirectToAction(nameof(ProductController.Page), nameof(Product), new { numberOfProductsPerPage = 10, pageNumber = 1 });

                var requestUserInfo = logInViewModel.ReturnUrl.Contains('?') ?
                    $"&userName={logInViewModel.Login}" : $"?userName={logInViewModel.Login}";
                return Redirect($"{logInViewModel.ReturnUrl}{requestUserInfo}");
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(logInViewModel);
        }

        public IActionResult Register(string returnUrl) => View(new RegistrationViewModel { ReturnUrl = returnUrl});

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registrationViewModel)
        {
            registrationViewModel.Login = registrationViewModel.Login.Trim();
            registrationViewModel.Password = registrationViewModel.Password.Trim();
            registrationViewModel.PasswordConfirm = registrationViewModel.PasswordConfirm.Trim();

            if (registrationViewModel.Password == registrationViewModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            var existingUser = usersManager.FindByNameAsync(registrationViewModel.Login).Result;

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(registrationViewModel);

            var user = new User { UserName = registrationViewModel.Login };

            var result = usersManager.CreateAsync(user, registrationViewModel.Password).Result;

            if (result.Succeeded)
            {
                usersManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                signInManager.SignInAsync(user, false).Wait();

                if (registrationViewModel.ReturnUrl == null)
                    return RedirectToAction(nameof(ProductController.Page), nameof(Product), new { numberOfProductsPerPage = 10, pageNumber = 1 });

                var requestUserInfo = registrationViewModel.ReturnUrl.Contains('?') ?
                    $"&userName={registrationViewModel.Login}" : $"?userName={registrationViewModel.Login}";
                return Redirect($"{registrationViewModel.ReturnUrl}{requestUserInfo}");
            }

            ModelState.AddModelError("", "Не удалось создать аккаунт");
            return View(registrationViewModel);
        }

        public IActionResult LogOut()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(ProductController.Page), nameof(Product), new { numberOfProductsPerPage = 10, pageNumber = 1 });
        }
    }
}