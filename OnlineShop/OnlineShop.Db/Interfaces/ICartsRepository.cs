using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Cart? TryGetByUserId(string userId);
        void AddProduct(Product product, string userId);
        void ChangeProductAmount(string userId, Guid cartItemId, int difference);
        void ClearCartByUserId(string userId);
    }
}
