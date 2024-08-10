using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoritesRepository
    {
        Task AddAsync(User user, Product product);
        Task RemoveAsync(User user, Product product);
        Task<List<Product>> GetByUserIdAsync(string userId);
    }
}
