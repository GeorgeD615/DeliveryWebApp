using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public interface ICartsRepository
    {
        Cart GetByUserId(int userId);
        void AddProduct(Product product, int userId);
        void ClearCart(int userId);
    }
}
