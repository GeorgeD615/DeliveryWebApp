using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<int> GetAmountAsync();
        Task<Product?> TryGetByIdAsync(Guid productId);
        Task<List<Product>> GetPageOfProductsAsync(int numberOfProductsPerPage, int pageNumber, int amountOfPages);
        Task DeleteProductAsync(Guid productId);
        Task EditAsync(Product product);
        Task AddAsync(Product product);
        Task<List<Product>> SearchByNameAsync(string name);
    }
}
