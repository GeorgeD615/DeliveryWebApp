using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public interface IUserRepository 
    {
        User? TryGetById(int userId);
        List<Product> GetFavorites(int userId);
        void AddFavorite(int userId, Product product);
        void RemoveFavorite(int userId, Product product);
        List<Address> GetAddresses(int userId);
        Address TryGetAddress(int userId, int addressId);
        void AddAddress(int userId, Address address);
        void RemoveAddress(int userId, int addressId);
        void SetLastAddress(int userId, Address address);
    } 
}
