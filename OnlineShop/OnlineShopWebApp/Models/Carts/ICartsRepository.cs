using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public interface ICartsRepository
    {
        Cart? TryGetByUserId(Guid userId);
        void AddProduct(Product product, Guid userId);
        void ChangeProductAmount(Guid cartId, Guid cartItemId, int difference);
        void ClearCart(Guid userId);
    }
}
