using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Roles
{
    public class RolesRepository : IRolesRepository
    {
        private static readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Data\\Roles.json";
        private List<Role> roles;
        public RolesRepository()
        {
            using var reader = new StreamReader(dataJsonFilePath);
            roles = JsonConvert.DeserializeObject<List<Role>>(reader.ReadToEnd());
        }
        public List<Role> GetAll() => roles;

        public Role? TryGetById(Guid id) => roles.FirstOrDefault(roles => roles.Id == id);
        public bool IsExisting(string name) => roles.Any(role => role.Name == name);
        public void AddRole(Role role)
        {
            roles.Add(role);
            SaveRolesIntoJson();
        }
        public void RemoveById(Guid id)
        {
            var role = TryGetById(id);

            if (role == null || role.Name.ToLower() == "admin")
                return;
            
            roles.Remove(role);
            SaveRolesIntoJson();
        }

        public Role? TryGetByName(string name) => roles.FirstOrDefault(role => role.Name == name);

        private void SaveRolesIntoJson()
        {
            using var writer = new StreamWriter(dataJsonFilePath, false);
            writer.Write(JsonConvert.SerializeObject(roles, Formatting.Indented));
        }
    }
}
