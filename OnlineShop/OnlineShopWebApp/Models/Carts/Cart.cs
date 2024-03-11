using OnlineShopWebApp.Models.Products;
using System.Drawing;

namespace OnlineShopWebApp.Models.Carts
{
    public class Cart
    {
        private List<CartItem> cartItems = new List<CartItem>();
        public int UserId { get; }
        public Cart(int userId)
        {
            UserId = userId;
        }
        public List<CartItem> GetAll() => cartItems;
        public void AddProduct(Product product)
        {
            if (!cartItems.Select(item => item.Product).Contains(product))
            {
                cartItems.Add(new CartItem(product));
                return;
            }
            
            cartItems.First(item => item.Product == product).Amount += 1;
        }
        public int Size { get {  return cartItems.Count; } }
        public decimal Cost { get { return cartItems.Sum(item => item.Cost); } }
    }
}
