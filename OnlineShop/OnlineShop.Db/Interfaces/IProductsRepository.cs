using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        int GetAmount();
        Product? TryGetById(Guid productId);
        List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages);
        public void DeleteProduct(Guid productId);
        public void Edit(Product product);
        public void Add(Product product);
        List<Product> SearchByName(string name);
    }
}
