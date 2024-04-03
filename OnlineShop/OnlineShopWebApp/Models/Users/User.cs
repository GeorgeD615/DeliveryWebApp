using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public class User
    {
        private static int nextId = 0;
        public int Id { get; }
        public string Name { get; set; }
        public string Login { get; set; }
        public List<Product> Favorites { get; set; }
        public List<Address> Addresses { get; set; }
        public Address? LastAddress { get; set; }

        public User(string name, string login)
        {
            Id = ++nextId;
            Name = name;
            Favorites = new();
            Addresses = new();
            Login = login;
        }
    }
}
