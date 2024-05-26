using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Implementations
{
    public class RolesDbRepository : IRolesRepository
    {
        private readonly DatabaseContext databaseContext;
        public RolesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public List<Role> GetAll() => databaseContext.Roles.AsNoTracking().ToList();

        public Role? TryGetById(Guid? id)
        {
            return databaseContext.Roles.FirstOrDefault(roles => roles.Id == id);
        }
        public bool IsExisting(string name)
        {
            return databaseContext.Roles.Any(role => role.Name == name);
        }
        public void AddRole(Role role)
        {
            databaseContext.Roles.Add(role);
            databaseContext.SaveChanges();
        }
        public void RemoveById(Guid id)
        {
            var role = TryGetById(id);

            if (role == null || role.Name.ToLower() == "admin")
                return;
            
            databaseContext.Roles.Remove(role);
            databaseContext.SaveChanges();
        }

        public Role? TryGetByName(string name)
        {
            return databaseContext.Roles.FirstOrDefault(role => role.Name == name);
        }
    }
}
