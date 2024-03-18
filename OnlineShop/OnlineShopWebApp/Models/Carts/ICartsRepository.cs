using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public interface ICartsRepository
    {
        Cart? TryGetById(int cartId);
        Cart? TryGetByUserId(int userId);
        void AddProduct(Product product, int userId);
        void ChangeProductAmount(int cartId, Guid cartItemId, int difference);
        void ClearCart(int userId);
    }
}
