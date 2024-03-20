using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Users
{
    public interface IUserRepository 
    {
        User? TryGetById(int userId);
        List<Product> GetFavorites(int userId);
        void AddFavorite(int userId, Product favoriteProduct);
        void RemoveFavorite(int userId, Product favoriteProduct);
    } 
}
