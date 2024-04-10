using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public interface IUserRepository 
    {
        User? TryGetById(Guid userId);
        User? TryGetByLogin(string login);
        List<Product> GetFavorites(Guid userId);
        void AddFavorite(Guid userId, Product product);
        void RemoveFavoriteById(Guid userId, Guid productId);
        List<Address> GetAddresses(Guid userId);
        Address TryGetAddress(Guid userId, Guid addressId);
        void AddAddress(Guid userId, Address address);
        void RemoveAddress(Guid userId, Guid addressId);
        void SetLastAddress(Guid userId, Address address);
        void Add(User user);
    } 
}
