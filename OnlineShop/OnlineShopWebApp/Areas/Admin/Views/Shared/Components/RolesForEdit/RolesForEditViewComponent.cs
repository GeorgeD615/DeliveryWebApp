using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles
{
    public class RolesForEditViewComponent : ViewComponent
    {
        private readonly IRolesRepository rolesRepository;

        public RolesForEditViewComponent(IRolesRepository rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var roles = rolesRepository.GetAll();
            return View("Roles", roles.Select(role => role.ToRoleViewModel()).ToList());
        }
    }
}
