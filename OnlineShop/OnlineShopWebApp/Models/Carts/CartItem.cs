using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }

        public CartItem(Product product, int amount = 1)
        {
            Product = product;
            Amount = amount;
        }

        public decimal Cost { 
            get 
            {
                return Product.Cost * Amount;
            } 
        }
    }
}
