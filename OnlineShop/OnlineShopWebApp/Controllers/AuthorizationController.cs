using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Roles;
using OnlineShopWebApp.Models.Users;
using OnlineShopWebApp.Models.ViewModels;
using System.Reflection.Metadata;

namespace OnlineShopWebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IRolesRepository rolesRepository;

        public AuthorizationController(IUserRepository userRepository, IRolesRepository rolesRepository)
        {
            this.userRepository = userRepository;
            this.rolesRepository = rolesRepository;
        }
        public IActionResult LogIn() => View();

        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInViewModel)
        {
            logInViewModel.Login = logInViewModel.Login.Trim();
            logInViewModel.Password = logInViewModel.Password.Trim();

            if (logInViewModel.Password == logInViewModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            var user = userRepository.TryGetByLogin(logInViewModel.Login);

            if (user == null)
                ModelState.AddModelError("", "Пользователя с таким логином не существует");

            if (user.Password != logInViewModel.Password)
                ModelState.AddModelError("", "Неверный пароль");

            if (!ModelState.IsValid)
                return View(logInViewModel);

            CommonData.currentUserId = user.Id;
            CommonData.currentUserRoleId = user.RoleId;

            return RedirectToAction("Page", "Product", new { numberOfProductsPerPage = 10, pageNumber = 1 });
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registrationViewModel)
        {
            registrationViewModel.Login = registrationViewModel.Login.Trim();
            registrationViewModel.Password = registrationViewModel.Password.Trim();
            registrationViewModel.PasswordConfirm = registrationViewModel.PasswordConfirm.Trim();

            if (registrationViewModel.Password == registrationViewModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            var exictingUser = userRepository.TryGetByLogin(registrationViewModel.Login);

            if (exictingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if(!ModelState.IsValid)
                return View(registrationViewModel);

            var userRoleId = rolesRepository.TryGetByName("user").Id;

            var user = new User(registrationViewModel.Login, registrationViewModel.Password, userRoleId);

            userRepository.Add(user);

            CommonData.currentUserId = user.Id;
            CommonData.currentUserRoleId = CommonData.userRoleId;

            return RedirectToAction("Page", "Product", new { numberOfProductsPerPage = 10, pageNumber = 1 });
        }
    }
}
