using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Users;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> usersManager;
        public UsersController(UserManager<User> usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var users = usersManager.Users.ToList();

            var usersViewModels = users.
                Select(user => user.
                ToUserViewModel(GetUserRole(user))).ToList();

            return View(usersViewModels);
        }

        private string? GetUserRole(User user) => usersManager.GetRolesAsync(user).Result?.FirstOrDefault();

        public IActionResult ShowUser(string userId)
        {
            var user = usersManager.FindByIdAsync(userId).Result;
            return View(user!.ToUserViewModel(usersManager.GetRolesAsync(user).Result?.FirstOrDefault()));
        }

        public IActionResult ChangePassword(string userId) => View(new ChangePasswordViewModel() { UserId = userId });

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var user = usersManager.FindByIdAsync(model.UserId).Result;

            if (user.UserName == model.Password)
                ModelState.AddModelError("", "Пароль и логин не должны совпадать.");

            if (!ModelState.IsValid)
                return View(model);

            var newHashPassword = usersManager.PasswordHasher.HashPassword(user!, model.Password);
            user!.PasswordHash = newHashPassword;
            usersManager.UpdateAsync(user).Wait();

            return RedirectToAction(nameof(ShowUser), new { model.UserId });
        }

        public IActionResult Edit(string userId)
        {
            var user = usersManager.FindByIdAsync(userId.ToString()).Result;
            var roleName = usersManager.GetRolesAsync(user).Result.FirstOrDefault();
            var editUserVM = new EditUserViewModel { UserId = userId, Login = user.UserName, Role = roleName };
            return View(editUserVM);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel userVM)
        {
            var user = usersManager.FindByIdAsync(userVM.UserId).Result;

            if (user != null && user.Id != userVM.UserId.ToString())
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(userVM);

            user.UserName = userVM.Login;
            var currentRole = usersManager.GetRolesAsync(user).Result.FirstOrDefault();

            if (currentRole != null)
                usersManager.RemoveFromRoleAsync(user, currentRole).Wait();

            usersManager.AddToRoleAsync(user, userVM.Role).Wait();

            usersManager.UpdateAsync(user).Wait();

            return RedirectToAction(nameof(ShowUser), new { userVM.UserId });
        }

        public IActionResult Remove(string userId)
        {
            var user = usersManager.FindByIdAsync(userId).Result;
            usersManager.DeleteAsync(user!).Wait();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create() => View(new CreateUserViewModel() { RegistrationModel = new RegistrationViewModel() });

        [HttpPost]
        public IActionResult Create(CreateUserViewModel createUserViewModel)
        {
            if (createUserViewModel.RegistrationModel.Password == createUserViewModel.RegistrationModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            var existingUser = usersManager.FindByNameAsync(createUserViewModel.RegistrationModel.Login).Result;

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(createUserViewModel);

            var user = new User() { UserName = createUserViewModel.RegistrationModel.Login };
            usersManager.CreateAsync(user, createUserViewModel.RegistrationModel.Password).Wait();
            usersManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();

            return RedirectToAction(nameof(Index));
        }
    }
}