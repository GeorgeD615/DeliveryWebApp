using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly UserManager<User> usersManager;

        public RolesController(RoleManager<IdentityRole> rolesManager, UserManager<User> usersManager)
        {
            this.rolesManager = rolesManager;
            this.usersManager = usersManager;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(RoleViewModel role)
        {
            var roleName = role.Name.Trim().ToLower();

            if (rolesManager.RoleExistsAsync(roleName).Result)
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View(role);

            rolesManager.CreateAsync(new IdentityRole { Name = roleName, }).Wait();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveById(string roleId)
        {
            var role = rolesManager.FindByIdAsync(roleId).Result;
            var usersInRole = usersManager.GetUsersInRoleAsync(role.Name).Result;

            if(usersInRole.Count > 0)
                ModelState.AddModelError("", "Данная роль уже присвоена какому-то пользователю.");

            if (!ModelState.IsValid)
                return View(nameof(Index));


            if (role != null)
                rolesManager.DeleteAsync(role).Wait();

            return RedirectToAction(nameof(Index));
        }
    }
}