using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoritesRepository
    {
        void Add(User user, Product product);
        void Remove(User user, Product product);
        List<Product> GetByUserId(string userId);
    }
}
