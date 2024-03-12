using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public class Cart
    {
        private List<CartItem> cartItems = new();
        public int UserId { get; }
        public Cart(int userId)
        {
            UserId = userId;
        }
        public List<CartItem> GetAll() => cartItems;
        public void AddProduct(Product product)
        {
            if (!cartItems.Any(item => item.Product == product))
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
