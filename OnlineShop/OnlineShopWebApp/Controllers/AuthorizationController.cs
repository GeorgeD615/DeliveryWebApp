using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUsersRepository usersRepository;
        private readonly IRolesRepository rolesRepository;

        public AuthorizationController(IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            this.usersRepository = usersRepository;
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

            var user = usersRepository.TryGetByLogin(logInViewModel.Login);

            if (user == null)
            {
                ModelState.AddModelError("", "Пользователя с таким логином не существует");
                return View(logInViewModel);
            }
            
            if (user.Password != logInViewModel.Password)
                ModelState.AddModelError("", "Неверный пароль");

            if (!ModelState.IsValid)
                return View(logInViewModel);

            CommonData.CurrentUserId = user.Id;
            CommonData.CurrentUserRoleId = user.Role.Id;

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

            var existingUser = usersRepository.TryGetByLogin(registrationViewModel.Login);

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if(!ModelState.IsValid)
                return View(registrationViewModel);

            var userRole = rolesRepository.TryGetByName("user");

            if (userRole == null)
            {
                userRole = new Role() { Name = "user" };
                CommonData.UserRoleId = userRole.Id;
                rolesRepository.AddRole(userRole);
            }

            var user = new User
            {
                Login = registrationViewModel.Login,
                Password = registrationViewModel.Password,
                Role = userRole
            };

            usersRepository.Add(user);

            CommonData.CurrentUserId = user.Id;
            CommonData.CurrentUserRoleId = CommonData.UserRoleId;

            return RedirectToAction("Page", "Product", new { numberOfProductsPerPage = 10, pageNumber = 1 });
        }
    }
}
