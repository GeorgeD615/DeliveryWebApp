using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Carts
{
    public static class CartsRepository
    {
        private static List<Cart> carts = new();
        public static Cart GetByUserId(int userId)
        {
            var cart = carts.FirstOrDefault(cart => cart.UserId == userId);

            if (cart == null)
            {
                cart = new Cart(userId);
                carts.Add(cart);
            }

            return cart;
        }
        public static void AddProduct(Product product, int userId)
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
        public static void ClearCart(int userId) => GetByUserId(userId).Items.Clear();
    }
}
