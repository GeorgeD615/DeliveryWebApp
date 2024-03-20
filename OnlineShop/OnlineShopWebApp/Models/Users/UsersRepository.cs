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
        public void AddFavorite(int userId, Product favoriteProduct) {
            var user = TryGetById(userId);
            if (user == null || user.Favorites.Contains(favoriteProduct))
                return;
            user.Favorites.Add(favoriteProduct);
        }

        public void RemoveFavorite(int userId, Product favoriteProduct) => TryGetById(userId)?.Favorites.Remove(favoriteProduct);
    }
}
