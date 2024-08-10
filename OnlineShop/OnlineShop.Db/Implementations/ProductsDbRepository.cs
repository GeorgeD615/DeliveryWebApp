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

        public async Task<List<Product>> GetAllAsync() => await databaseContext.Products.Include(product => product.Images).AsNoTracking().ToListAsync();
        public async Task<int> GetAmountAsync() => await databaseContext.Products.CountAsync();
        public async Task<Product?> TryGetByIdAsync(Guid productId)
        {
            return await databaseContext.Products.Include(product => product.Images).
                FirstOrDefaultAsync(p => p.Id == productId);
        }
        public async Task<List<Product>> GetPageOfProductsAsync(int numberOfProductsPerPage, int pageNumber, int amountOfPages)
        {
            int lastIndex = pageNumber < amountOfPages ? 
                numberOfProductsPerPage : 
                await databaseContext.Products.CountAsync() - numberOfProductsPerPage * (pageNumber - 1);
            var products = await databaseContext.Products.Include(product => product.Images).ToListAsync();
            return products.GetRange((pageNumber - 1) * numberOfProductsPerPage, lastIndex);
        }
        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await TryGetByIdAsync(productId);

            if (product == null)
                throw new Exception("Товар не найден");

            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            var existingProduct = await TryGetByIdAsync(product.Id);

            if (existingProduct == null)
                return;

            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;

            foreach(var image in product.Images)
            {
                image.ProductId = product.Id;
                databaseContext.Image.Add(image);
            }

            await databaseContext.SaveChangesAsync();
        }
        public async Task AddAsync(Product product)
        {
            databaseContext.Products.Add(product);
            await databaseContext.SaveChangesAsync();
        }
        public async Task<List<Product>> SearchByNameAsync(string name) 
        {
            return await databaseContext.Products.Include(product => product.Images)
                .Where(product => EF.Functions.Like(product.Name, $"%{name}%"))
                .ToListAsync();
        }
    }
}
