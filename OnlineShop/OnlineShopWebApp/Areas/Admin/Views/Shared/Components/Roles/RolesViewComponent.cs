using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles
{
    public class RolesViewComponent : ViewComponent
    {
        private readonly RoleManager<IdentityRole> rolesManager;

        public RolesViewComponent(RoleManager<IdentityRole> rolesManager)
        {
            this.rolesManager = rolesManager;
        }

        public IViewComponentResult Invoke()
        {
            var roles = rolesManager.Roles;
            return View("Roles", roles.Select(role => role.ToRoleViewModel()).ToList());
        }
    }
}