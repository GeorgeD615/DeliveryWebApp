using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public class UsersRepository : IUserRepository
    {
        private List<User> users = new();
        public UsersRepository()
        {
            users.Add(new User("Георгий"));
        }
        public User? TryGetById(int userId) => users.FirstOrDefault(user => user.Id == userId);
        public List<Product> GetFavorites(int userId) => TryGetById(userId)?.Favorites;
        public void AddFavorite(int userId, Product product) {
            var user = TryGetById(userId);
            if (user == null || user.Favorites.Contains(product))
                return;
            user.Favorites.Add(product);
        }

        public void RemoveFavorite(int userId, Product product) => TryGetById(userId)?.Favorites.Remove(product);
    }
}
