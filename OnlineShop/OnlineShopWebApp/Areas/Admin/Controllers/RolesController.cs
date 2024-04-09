using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IRolesRepository rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(RoleViewModel role)
        {
            var roleName = role.Name.Trim().ToLower();

            if (rolesRepository.IsExisting(roleName))
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View(role);

            rolesRepository.AddRole(new Role(roleName));

            return RedirectToAction("Index");
        }

        public IActionResult RemoveById(int roleId)
        {
            rolesRepository.RemoveById(roleId);
            return RedirectToAction("Index");
        }
    }
}
