using Newtonsoft.Json;

namespace OnlineShopWebApp.Models
{
    public static class ProductsInfo
    {
        private static readonly string dataJsonFilePath = "C:\\Users\\OMEN\\source\\repos\\ASP.NET_Core_7.0_kurators\\OnlineShop\\OnlineShopWebApp\\wwwroot\\ProductsInfo.json";
        public static List<Product> Products { get; set; }

        static ProductsInfo()
        {
            try
            {
                using var reader = new StreamReader(dataJsonFilePath);
                Products = JsonConvert.DeserializeObject<List<Product>>(reader.ReadToEnd());
            }
            catch
            {
                //TODO : залогировать информацию об ошибке
                //throw?
            }
            finally
            {
                Products ??= new List<Product>()
                {
                    {new Product("Эчпочмак", 90, "Очень вкусная булочка")},
                    {new Product("Хачапури", 125, "Очень вкусная лепёшка")},
                    {new Product("Хинкали", 300, "Очень вкусные пельмени")}
                };
            }
        }

        public static Product? GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public static List<Product> GetPageOfProducts(int productsNum, int pageNum)
        {
            return Products.GetRange(((pageNum - 1) * productsNum), productsNum);
        }
    }
}
