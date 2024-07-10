using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopWebApp.Models.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles
{
    public class RolesForEditViewComponent : ViewComponent
    {
        private readonly RoleManager<IdentityRole> rolesManager;

        public RolesForEditViewComponent(RoleManager<IdentityRole> rolesManager)
        {
            this.rolesManager = rolesManager;
        }

        public IViewComponentResult Invoke(string currentRoleName)
        {
            var roles = rolesManager.Roles.Select(role => role.ToRoleViewModel()).ToList();

            if (!currentRoleName.IsNullOrEmpty())
            {
                var currentRole = roles.FirstOrDefault(role => role.Name == currentRoleName);
                roles.Remove(currentRole);
                roles.Insert(0, currentRole);
            }

            return View("Roles", roles);
        }
    }
}