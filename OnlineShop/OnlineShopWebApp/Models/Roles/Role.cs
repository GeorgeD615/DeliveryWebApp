using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Roles
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        [JsonConstructor]
        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
