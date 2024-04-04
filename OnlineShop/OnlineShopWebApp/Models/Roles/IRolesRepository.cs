using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineShopWebApp.Models.Roles
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        void AddRole(Role role);
        bool IsExisting(string name);
        void RemoveRole(int id);
    }
}