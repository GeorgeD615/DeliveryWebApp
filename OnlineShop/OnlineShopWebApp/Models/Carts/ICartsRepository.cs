using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public interface ICartsRepository
    {
        public Cart GetByUserId(int userId);
        public void AddProduct(Product product, int userId);
        public void ClearCart(int userId);
    }
}
