namespace OnlineShopWebApp.Models
{
    public static class Cart
    {
        private static Dictionary<Product, int> products = new Dictionary<Product, int>();
        public static Dictionary<Product, int> GetAll() => products;
        public static void AddProduct(Product product) 
        { 
            if(products.ContainsKey(product))
                ++products[product];
            else
                products[product] = 1;
        }
    }
}
