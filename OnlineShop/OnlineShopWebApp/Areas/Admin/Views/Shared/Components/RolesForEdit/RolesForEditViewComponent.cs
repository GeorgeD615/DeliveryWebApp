using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;

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
            return View("Roles", roles);
        }
    }
}
