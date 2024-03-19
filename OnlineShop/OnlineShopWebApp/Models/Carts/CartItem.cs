using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartItem
    {
        private static int nextId = 0;
        public int Id { get; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get => Product.Cost * Amount; }
        public CartItem(Product product, int amount = 1)
        {
            Id = ++nextId;
            Product = product;
            Amount = amount;
        }
    }
}
