namespace OnlineShopWebApp.Models
{
    public static class ProductsInfo
    {
        public static Dictionary<int, Product> Products { get; set; } = new Dictionary<int, Product>()
        {
            {1, new Product("Эчпочмак", 90, "Очень вкусная булочка")},
            {2, new Product("Хачапури", 125, "Очень вкусная лепёшка")},
            {3, new Product("Хинкали", 300, "Очень вкусные пельмени")}
        };
    }
}
