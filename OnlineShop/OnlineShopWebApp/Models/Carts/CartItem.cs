using OnlineShopWebApp.Models.Products;
using Newtonsoft.Json;

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

        [JsonConstructor]
        public CartItem(Guid id, Product product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
        }
    }
}
