namespace OnlineShopWebApp.Models
{
    public static class ProductsInfo
    {
        public static List<Product> Products { get; set; } = new List<Product>()
        {
            {new Product("Эчпочмак", 90, "Очень вкусная булочка")},
            {new Product("Хачапури", 125, "Очень вкусная лепёшка")},
            {new Product("Хинкали", 300, "Очень вкусные пельмени")}
        };

        public static Product? GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
