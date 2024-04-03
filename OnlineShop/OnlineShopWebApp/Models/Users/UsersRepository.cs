using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public class UsersRepository : IUserRepository
    {
        private List<User> users = new();
        public UsersRepository()
        {
            users.Add(new User("Георгий", "George"));
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

        public List<Address> GetAddresses(int userId) => TryGetById(userId)?.Addresses;
        public Address TryGetAddress(int userId, int addressId) => GetAddresses(userId).FirstOrDefault(address => address.Id == addressId);
        public void AddAddress(int userId, Address address)
        {
            var user = TryGetById(userId);
            if (user == null || user.Addresses.Any(oldAddress => oldAddress.City == address.City &&
                                                                oldAddress.Street == address.Street &&
                                                                oldAddress.House == address.House &&
                                                                oldAddress.Flat == address.Flat))
                return;
            user.Addresses.Add(address);
            user.LastAddress = address;
        }

        public void RemoveAddress(int userId, int addressId)
        {
            var user = TryGetById(userId);
            var address = user?.Addresses.FirstOrDefault(address => address.Id == addressId);

            if (user?.LastAddress == address)
                user.LastAddress = user.Addresses.Count > 1 ? user.Addresses[0] : null;

            user?.Addresses.Remove(address);
        }

        public void SetLastAddress(int userId, Address address) => TryGetById(userId).LastAddress = address;
    }
}
