namespace OnlineShopWebApp.Models.Roles
{
    public class RolesRepository : IRolesRepository
    {
        private List<Role> roles;
        public RolesRepository()
        {
            roles = new List<Role>() { new Role("admin"), new Role("user") };
        }
        public List<Role> GetAll() => roles;

        public Role? TryGetById(int id) => roles.FirstOrDefault(roles => roles.Id == id);
        public bool IsExisting(string name) => roles.Any(role => role.Name == name);
        public void AddRole(Role role) => roles.Add(role);
        public void RemoveById(int id)
        {
            var role = TryGetById(id);

            if (role == null || role?.Name.ToLower() == "admin")
                return;
            
            roles.Remove(role);
        }
    }
}
