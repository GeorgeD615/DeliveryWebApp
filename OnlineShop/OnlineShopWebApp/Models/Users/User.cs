using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public class User
    {
        private static int nextId = 0;
        public int Id { get; }
        public string Name { get; set; }
        public List<Product> Favorites { get; set; }

        public User(string name)
        {
            Id = ++nextId;
            Name = name;
            Favorites = new();
        }
    }
}
