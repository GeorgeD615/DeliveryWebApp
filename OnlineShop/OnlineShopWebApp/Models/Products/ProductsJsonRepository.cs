using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Products
{
    public class ProductsJsonRepository
    {
        private static readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\ProductsInfo.json";
        public static List<Product> Products
        {
            get
            {
                using var reader = new StreamReader(dataJsonFilePath);
                return JsonConvert.DeserializeObject<List<Product>>(reader.ReadToEnd());
            }
            set
            {
                using var writer = new StreamWriter(dataJsonFilePath, false);
                writer.Write(JsonConvert.SerializeObject(value));
            }
        }
    }
}
