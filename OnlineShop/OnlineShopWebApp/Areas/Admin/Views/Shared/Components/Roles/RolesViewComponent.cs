using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles
{
    public class RolesViewComponent : ViewComponent
    {
        private readonly IRolesRepository rolesRepository;

        public RolesViewComponent(IRolesRepository rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var roles = rolesRepository.GetAll();
            return View("Roles", roles);
        }
    }
}
