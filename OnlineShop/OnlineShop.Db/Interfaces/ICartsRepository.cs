using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Cart? TryGetByUserId(Guid userId);
        void AddProduct(Product product, Guid userId);
        void ChangeProductAmount(Guid cartId, Guid cartItemId, int difference);
        void ClearCart(Guid userId);
    }
}
