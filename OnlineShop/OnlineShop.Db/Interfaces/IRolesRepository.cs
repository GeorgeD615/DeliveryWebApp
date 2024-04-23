using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
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