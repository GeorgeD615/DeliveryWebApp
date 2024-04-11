using Newtonsoft.Json;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public class UsersRepository : IUsersRepository
    {
        private static readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Data\\Users.json";
        
        private List<User> users;

        public UsersRepository()
        {
            using var reader = new StreamReader(dataJsonFilePath);
            users = JsonConvert.DeserializeObject<List<User>>(reader.ReadToEnd());
        }
        public User? TryGetById(Guid userId) => users?.FirstOrDefault(user => user.Id == userId);
        public List<User> GetAll() => users;
        public List<Product>? TryGetFavorites(Guid userId) => TryGetById(userId)?.Favorites;
        public void AddFavorite(Guid userId, Product product) {
            var user = TryGetById(userId);
            if (user?.Favorites?.Any(favorite => favorite.Id == product.Id) ?? false)
                return;
            user?.Favorites.Add(product);
            SaveUsersIntoJson();
        }

        public void RemoveFavoriteById(Guid userId, Guid productId)
        {
            TryGetById(userId)?.Favorites.RemoveAll(product => product.Id == productId);
            SaveUsersIntoJson();
        }

        public List<Address>? TryGetAddresses(Guid userId) => TryGetById(userId)?.Addresses;
        public Address? TryGetAddress(Guid userId, Guid addressId) => TryGetAddresses(userId)?.FirstOrDefault(address => address.Id == addressId);
        public void AddAddress(Guid userId, Address address)
        {
            var user = TryGetById(userId);
            if (user == null || user.Addresses.Any(oldAddress => oldAddress.City == address.City &&
                                                                oldAddress.Street == address.Street &&
                                                                oldAddress.House == address.House &&
                                                                oldAddress.Flat == address.Flat))
                return;
            user.Addresses.Add(address);
            user.LastAddress = address;

            SaveUsersIntoJson();
        }

        public void RemoveAddress(Guid userId, Guid addressId)
        {
            var user = TryGetById(userId);
            var address = user?.Addresses?.FirstOrDefault(address => address.Id == addressId);

            if (user != null && user.LastAddress == address)
                user.LastAddress = user.Addresses.Count > 1 ? user.Addresses[0] : null;

            if(address != null)
                user?.Addresses.Remove(address);

            SaveUsersIntoJson();
        }

        public void SetLastAddress(Guid userId, Address address)
        {
            var user = TryGetById(userId);
            if (user == null)
                return;
            user.LastAddress = address;
            SaveUsersIntoJson();
        }

        private void SaveUsersIntoJson()
        {
            using var writer = new StreamWriter(dataJsonFilePath, false);
            writer.Write(JsonConvert.SerializeObject(users, Formatting.Indented));
        }

        public void Add(User user)
        {
            users?.Add(user);
            SaveUsersIntoJson();
        }

        public User? TryGetByLogin(string login) => users?.FirstOrDefault(user => user.Login == login);

        public void ChangePassword(Guid userId, string password)
        {
            var user = TryGetById(userId);

            if (user == null)
                return;

            user.Password = password;
            SaveUsersIntoJson();
        }

        public void Edit(EditUserModel editModel)
        {
            var user = TryGetById(editModel.UserId);

            if (user == null) 
                return;

            user.Login = editModel.Login;

            user.Role = editModel.Role;

            SaveUsersIntoJson();
        }

        public void Remove(Guid userId)
        {
            users.RemoveAll(user => user.Id == userId);
            SaveUsersIntoJson();
        }
    }
}
