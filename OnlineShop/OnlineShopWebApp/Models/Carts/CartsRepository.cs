using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartsRepository : ICartsRepository
    {
        private List<Cart> carts = new();
        public Cart GetByUserId(int userId)
        {
            var cart = carts.FirstOrDefault(cart => cart.UserId == userId);

            if (cart == null)
            {
                cart = new Cart(userId);
                carts.Add(cart);
            }

            return cart;
        }
        public void AddProduct(Product product, int userId)
        {
            var cart = carts.FirstOrDefault(cart => cart.UserId == userId);
            
            if(cart == null)
            {
                cart = new Cart(userId);
                carts.Add(cart);
            }

            var itemInCart = cart.Items.FirstOrDefault(item => item.Product == product);

            if (itemInCart == null)
                cart.Items.Add(new CartItem(product));
            else
                itemInCart.Amount += 1;
        }
        public void ClearCart(int userId) => GetByUserId(userId).Items.Clear();
    }
}
