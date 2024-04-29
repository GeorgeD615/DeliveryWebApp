using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models.Users
{
    public class UsersDbRepository : IUsersRepository
    {
        private readonly DatabaseContext databaseContext;

        public UsersDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public User? TryGetById(Guid userId)
        {
            return databaseContext.Users
                .Include(user => user.Addresses)
                .Include(user => user.Favorites)
                .Include(user => user.Role)
                .FirstOrDefault(user => user.Id == userId);
        }
        public List<User> GetAll() => databaseContext.Users.AsNoTracking().Include(user => user.Role).ToList();
        public List<Product>? TryGetFavorites(Guid userId) => TryGetById(userId)?.Favorites;
        public void AddFavorite(Guid userId, Product product) {    
            var user = TryGetById(userId);

            if (user?.Favorites?.Any(favorite => favorite.Id == product.Id) ?? false)
                return;

            user?.Favorites.Add(product);
            databaseContext.SaveChanges();
        }

        public void RemoveFavoriteById(Guid userId, Guid productId)
        {
            TryGetById(userId)?.Favorites.RemoveAll(product => product.Id == productId);
            databaseContext.SaveChanges();
        }

        public List<Address>? TryGetAddresses(Guid userId) => TryGetById(userId)?.Addresses;
        public Address? TryGetAddress(Guid userId, Guid addressId)
        {
            return TryGetAddresses(userId)?.FirstOrDefault(address => address.Id == addressId);
        }
        public void AddAddress(Guid userId, Address address)
        {
            var user = TryGetById(userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            if (user.Addresses.Any(oldAddress => oldAddress.City == address.City &&
                                                                oldAddress.Street == address.Street &&
                                                                oldAddress.House == address.House &&
                                                                oldAddress.Flat == address.Flat))
                return;

            foreach(var a in user.Addresses)
                a.IsLast = false;

            address.IsLast = true;

            user.Addresses.Add(address);

            databaseContext.SaveChanges();
        }

        public void RemoveAddress(Guid userId, Guid addressId)
        {
            var user = TryGetById(userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            var address = user.Addresses.FirstOrDefault(address => address.Id == addressId);

            if (address == null)
                throw new Exception("Адрес не найден");

            bool isLast = address.IsLast;

            user.Addresses.Remove(address);

            if (isLast && user.Addresses.Any())
                user.Addresses[0].IsLast = true;

            databaseContext.SaveChanges();
        }

        public void SetLastAddress(Guid userId, Guid addressId)
        {
            var address = TryGetAddress(userId, addressId);

            if (address == null)
                throw new Exception("Адрес не найден");

            foreach(var a in TryGetAddresses(userId))
                a.IsLast = false;

            address.IsLast = true;

            databaseContext.SaveChanges();
        }

        public void Add(User user)
        {
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();
        }

        public User? TryGetByLogin(string login)
        {
            return databaseContext.Users.Include(user => user.Role).FirstOrDefault(user => user.Login == login);
        }

        public void ChangePassword(Guid userId, string password)
        {
            var user = TryGetById(userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.Password = password;
            databaseContext.SaveChanges();
        }

        public void Edit(User userModel)
        {
            var user = TryGetById(userModel.Id);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.Login = userModel.Login;

            user.Role = userModel.Role;

            databaseContext.SaveChanges();
        }

        public void Remove(Guid userId)
        {
            var user = TryGetById(userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            databaseContext.Users.Remove(user);
            databaseContext.SaveChanges();
        }
    }
}
