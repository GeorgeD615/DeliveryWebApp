using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult> Index()
        {
            var users = await usersManager.Users.ToListAsync();

            var usersViewModels = users.
                Select(user => user.
                ToUserViewModel(GetUserRoleAsync(user).Result)).ToList();

            return View(usersViewModels);
        }

        private async Task<string?> GetUserRoleAsync(User user) => (await usersManager.GetRolesAsync(user))?.FirstOrDefault();

        public async Task<ActionResult> ShowUserAsync(string userId)
        {
            var user = await usersManager.FindByIdAsync(userId);
            return View(user!.ToUserViewModel((await usersManager.GetRolesAsync(user))?.FirstOrDefault()));
        }

        public IActionResult ChangePassword(string userId) => View(new ChangePasswordViewModel() { UserId = userId });

        [HttpPost]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await usersManager.FindByIdAsync(model.UserId);

            if (user.UserName == model.Password)
                ModelState.AddModelError("", "Пароль и логин не должны совпадать.");

            if (!ModelState.IsValid)
                return View(model);

            var newHashPassword = usersManager.PasswordHasher.HashPassword(user!, model.Password);
            user!.PasswordHash = newHashPassword;
            await usersManager.UpdateAsync(user);

            return RedirectToAction("ShowUser", new { model.UserId });
        }

        public async Task<ActionResult> EditAsync(string userId)
        {
            var user = await usersManager.FindByIdAsync(userId.ToString());
            var roleName = (await usersManager.GetRolesAsync(user)).FirstOrDefault();
            var editUserVM = new EditUserViewModel { UserId = userId, Login = user.UserName, Role = roleName };
            return View(editUserVM);
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(EditUserViewModel userVM)
        {
            var user = await usersManager.FindByIdAsync(userVM.UserId);

            if (user != null && user.Id != userVM.UserId.ToString())
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(userVM);

            user.UserName = userVM.Login;
            var currentRole = (await usersManager.GetRolesAsync(user)).FirstOrDefault();

            if (currentRole != null)
                await usersManager.RemoveFromRoleAsync(user, currentRole);

            await usersManager.AddToRoleAsync(user, userVM.Role);

            await usersManager.UpdateAsync(user);

            return RedirectToAction("ShowUser", new { userVM.UserId });
        }

        public async Task<ActionResult> Remove(string userId)
        {
            var user = await usersManager.FindByIdAsync(userId);
            await usersManager.DeleteAsync(user!);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create() => View(new CreateUserViewModel() { RegistrationModel = new RegistrationViewModel() });

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateUserViewModel createUserViewModel)
        {
            if (createUserViewModel.RegistrationModel.Password == createUserViewModel.RegistrationModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            var existingUser = await usersManager.FindByNameAsync(createUserViewModel.RegistrationModel.Login);

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(createUserViewModel);

            var user = new User() { UserName = createUserViewModel.RegistrationModel.Login };
            await usersManager.CreateAsync(user, createUserViewModel.RegistrationModel.Password);
            await usersManager.AddToRoleAsync(user, Constants.UserRoleName);

            return RedirectToAction(nameof(Index));
        }
    }
}