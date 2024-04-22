using OnlineShopWebApp.Models.Products;
namespace OnlineShopWebApp.Models.Carts
{
    public class CartItemViewModel
    {
        public Guid Id { get; }
        public ProductViewModel Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get => Product.Cost * Amount; }

        public CartItemViewModel(Guid id, ProductViewModel product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
        }
    }
}
