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
        public async Task<ActionResult> Index(RoleViewModel role)
        {
            var roleName = role.Name.Trim().ToLower();

            if (await rolesManager.RoleExistsAsync(roleName))
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View(role);

            await rolesManager.CreateAsync(new IdentityRole { Name = roleName, });

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> RemoveByIdAsync(string roleId)
        {
            var role = await rolesManager.FindByIdAsync(roleId);
            var usersInRole = await usersManager.GetUsersInRoleAsync(role.Name);

            if(usersInRole.Count > 0)
                ModelState.AddModelError("", "Данная роль уже присвоена какому-то пользователю.");

            if (!ModelState.IsValid)
                return View(nameof(Index));


            if (role != null)
                await rolesManager.DeleteAsync(role);

            return RedirectToAction(nameof(Index));
        }
    }
}