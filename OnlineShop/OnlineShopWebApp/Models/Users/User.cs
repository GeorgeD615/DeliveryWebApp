using Newtonsoft.Json;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Models.Users
{
    public class User
    {
        public Guid Id { get; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Product> Favorites { get; set; }
        public List<Address> Addresses { get; set; }
        public Address? LastAddress { get; set; }
        public Role Role { get; set; }

        public User(string login, string password, Role role)
        {
            Id = Guid.NewGuid();
            Favorites = new();
            Addresses = new();
            LastAddress = null;
            Login = login;
            Password = password;
            Role = role;
        }

        [JsonConstructor]
        public User(Guid id, string login, List<Product> favorites, List<Address> addresses, Address lastAddress, string password, Role role)
        {
            Id = id;
            Login = login;
            Password = password;
            Favorites = favorites;
            Addresses = addresses;
            LastAddress = lastAddress;
            Role = role;
        }
    }
}
