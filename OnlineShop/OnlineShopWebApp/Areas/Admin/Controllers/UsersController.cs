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
            var users = usersManager.Users;
            return View(users.
                Select(user => user.
                ToUserViewModel(usersManager.GetRolesAsync(user).Result.Single())));
        }

        public IActionResult ShowUser(Guid userId)
        {
            var user = usersManager.FindByIdAsync(userId.ToString()).Result;
            return View(user!.ToUserViewModel());
        }

        public IActionResult ChangePassword(string userId) => View(new ChangePasswordViewModel() { UserId = userId });

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = usersManager.FindByIdAsync(model.UserId.ToString()).Result;

            var newHashPassword = usersManager.PasswordHasher.HashPassword(user!, model.Password);
            user!.PasswordHash = newHashPassword;
            usersManager.UpdateAsync(user).Wait();

            return RedirectToAction("ShowUser", new { model.UserId });
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
            var user = usersManager.FindByNameAsync(userVM.Login).Result;

            if (user != null && user.Id != userVM.UserId.ToString())
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(userVM);



            user.UserName = userVM.Login;
            var prevRole = usersManager.GetRolesAsync(user).Result.First();
            usersManager.RemoveFromRoleAsync(user, prevRole).Wait();
            usersManager.AddToRoleAsync(user, userVM.Role).Wait();

            usersManager.UpdateAsync(user).Wait();

            return RedirectToAction("ShowUser", new { userVM.UserId });
        }

        public IActionResult Remove(Guid userId)
        {
            var user = usersManager.FindByIdAsync(userId.ToString()).Result;
            usersManager.DeleteAsync(user!).Wait();
            return RedirectToAction("Index");
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
            usersManager.AddToRoleAsync(user, Constants.userRoleName);

            usersManager.CreateAsync(user).Wait();

            return RedirectToAction("Index");
        }
    }
}