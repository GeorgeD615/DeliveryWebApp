using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public interface IUsersRepository 
    {
        User? TryGetById(Guid userId);

        List<User> GetAll();
        User? TryGetByLogin(string login);
        List<Product>? TryGetFavorites(Guid userId);
        void AddFavorite(Guid userId, Product product);
        void RemoveFavoriteById(Guid userId, Guid productId);
        List<Address>? TryGetAddresses(Guid userId);
        Address? TryGetAddress(Guid userId, Guid addressId);
        void AddAddress(Guid userId, Address address);
        void RemoveAddress(Guid userId, Guid addressId);
        void SetLastAddress(Guid userId, Address address);
        void Add(User user);
        void ChangePassword(Guid userId, string password);
        void Edit(EditUserModel editModel);
        void Remove(Guid userId);
    } 
}
