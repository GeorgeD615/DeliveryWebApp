using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Implementations
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Product> GetAll() => databaseContext.Products.ToList();
        public int GetCount() => databaseContext.Products.Count();
        public Product? TryGetById(Guid productId) => databaseContext.Products.FirstOrDefault(p => p.Id == productId);
        public List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages)
        {
            int lastIndex = pageNumber < amountOfPages ? numberOfProductsPerPage : databaseContext.Products.Count() - numberOfProductsPerPage * (pageNumber - 1);
            return databaseContext.Products.ToList().GetRange((pageNumber - 1) * numberOfProductsPerPage, lastIndex);
        }
        public void DeleteProduct(Guid productId)
        {
            var product = TryGetById(productId);
            if (product == null)
                return;

            databaseContext.Products.Remove(product);
            databaseContext.SaveChanges();
        }

        public void Edit(Product product)
        {
            var exisyingProduct = TryGetById(product.Id);

            if (exisyingProduct == null)
                return;

            product.Name = exisyingProduct.Name;
            product.Cost = exisyingProduct.Cost;
            product.Description = exisyingProduct.Description;
            databaseContext.SaveChanges();
        }
        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public List<Product> SearchByName(string name)
            => GetAll().FindAll(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}
