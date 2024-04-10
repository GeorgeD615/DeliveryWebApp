using Newtonsoft.Json;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Data\\Products.json";

        private List<Product> products;

        public ProductsRepository()
        {
            using var reader = new StreamReader(dataJsonFilePath);
            products = JsonConvert.DeserializeObject<List<Product>>(reader.ReadToEnd());
        }

        public List<Product> GetAll() => products;
        public int GetCount() => products.Count;
        public Product? TryGetById(Guid productId) => products.FirstOrDefault(p => p.Id == productId);
        public List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages)
        {
            int lastIndex = pageNumber < amountOfPages ? numberOfProductsPerPage : products.Count - numberOfProductsPerPage * (pageNumber - 1);
            return products.GetRange((pageNumber - 1) * numberOfProductsPerPage, lastIndex);
        }
        public void DeleteProduct(Guid productId)
        {
            products.RemoveAll(p => p.Id == productId);
            SaveProductsIntoJson();
        }

        public void Edit(ProductViewModel productEditModel)
        {
            var product = TryGetById(productEditModel.Id);
            product.Name = productEditModel.Name;
            product.Cost = productEditModel.Cost;
            product.Description = productEditModel.Description;
            SaveProductsIntoJson();
        }
        public void Add(ProductViewModel productCreateModel)
        {
            var product = new Product(productCreateModel.Name, productCreateModel.Cost, productCreateModel.Description, "/images/products/eachpochmak.jpg");
            products.Add(product);
            SaveProductsIntoJson();
        }

        public List<Product> SearchByName(string name) 
            => products.FindAll(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        private void SaveProductsIntoJson()
        {
            using var writer = new StreamWriter(dataJsonFilePath, false);
            writer.Write(JsonConvert.SerializeObject(products, Formatting.Indented));
        }
    }
}
