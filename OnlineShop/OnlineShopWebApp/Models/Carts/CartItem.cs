using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartItem
    {
        public Guid Id { get; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get => Product.Cost * Amount; }

        public CartItem(Product product, int amount = 1)
        {
            Id = Guid.NewGuid();
            Product = product;
            Amount = amount;
        }
    }
}
