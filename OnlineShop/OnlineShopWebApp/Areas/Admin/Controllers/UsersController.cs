using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Users;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.adminRoleName)]
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
                ToUserViewModel(usersManager.GetRolesAsync(user).Result?.FirstOrDefault())).ToList();

            return View(usersViewModels);
        }

        public IActionResult ShowUser(string userId)
        {
            var user = usersManager.FindByIdAsync(userId).Result;
            return View(user!.ToUserViewModel(usersManager.GetRolesAsync(user).Result?.FirstOrDefault()));
        }

        public IActionResult ChangePassword(string userId) => View(new ChangePasswordViewModel() { UserId = userId });

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = usersManager.FindByIdAsync(model.UserId).Result;

            var newHashPassword = usersManager.PasswordHasher.HashPassword(user!, model.Password);
            user!.PasswordHash = newHashPassword;
            usersManager.UpdateAsync(user).Wait();

            return RedirectToAction(nameof(ShowUser), new { model.UserId });
        }

        public IActionResult Edit(string userId)
        {
            var user = usersManager.FindByIdAsync(userId.ToString()).Result;
            var editUserVM = new EditUserViewModel { UserId = userId, Login = user.UserName };
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
            var prevRole = usersManager.GetRolesAsync(user).Result.FirstOrDefault();

            if (prevRole != null)
                usersManager.RemoveFromRoleAsync(user, prevRole).Wait();

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
            usersManager.AddToRoleAsync(user, Constants.userRoleName).Wait();

            return RedirectToAction(nameof(Index));
        }
    }
}