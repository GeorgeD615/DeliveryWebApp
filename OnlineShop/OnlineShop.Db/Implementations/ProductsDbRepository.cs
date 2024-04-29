using Microsoft.EntityFrameworkCore;
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

        public List<Product> GetAll() => databaseContext.Products.AsNoTracking().ToList();
        public int GetCount() => databaseContext.Products.Count();
        public Product? TryGetById(Guid productId)
        {
            return databaseContext.Products.FirstOrDefault(p => p.Id == productId);
        }
        public List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages)
        {
            int lastIndex = pageNumber < amountOfPages ? numberOfProductsPerPage : databaseContext.Products.Count() - numberOfProductsPerPage * (pageNumber - 1);
            return databaseContext.Products.ToList().GetRange((pageNumber - 1) * numberOfProductsPerPage, lastIndex);
        }
        public void DeleteProduct(Guid productId)
        {
            var product = TryGetById(productId);

            if (product == null)
                throw new Exception("Товар не найден");

            databaseContext.Products.Remove(product);
            databaseContext.SaveChanges();
        }

        public void Edit(Product product)
        {
            var existingProduct = TryGetById(product.Id);

            if (existingProduct == null)
                return;

            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;
            existingProduct.ImagePath = product.ImagePath;
            databaseContext.SaveChanges();
        }
        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public List<Product> SearchByName(string name) 
        {
            return databaseContext.Products
                .Where(product => EF.Functions.Like(product.Name, $"%{name}%"))
                .ToList();
        }
    }
}
