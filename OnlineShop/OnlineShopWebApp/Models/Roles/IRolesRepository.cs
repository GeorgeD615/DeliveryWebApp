using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineShopWebApp.Models.Roles
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        void AddRole(Role role);
        bool IsExisting(string name);
        void RemoveById(Guid id);
        Role? TryGetById(Guid id);
        Role? TryGetByName(string name);
    }
}