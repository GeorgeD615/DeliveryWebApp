namespace OnlineShopWebApp.Models.Roles
{
    public class RolesRepository : IRolesRepository
    {
        private List<Role> roles;
        public RolesRepository()
        {
            roles = new List<Role>() { new Role("Admin") };
        }
        public List<Role> GetAll() => roles;
        public bool IsExisting(string name) => roles.Any(role => role.Name == name);
        public void AddRole(Role role) => roles.Add(role);

        public void RemoveRole(int id) => roles.RemoveAll(role => role.Id == id);
    }
}
