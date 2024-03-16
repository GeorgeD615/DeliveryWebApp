namespace OnlineShopWebApp.Models.Products
{
    public interface IProductsRepository
    {
        public List<Product> GetAll();
        public int GetCount();
        public Product? TryGetById(int productId);
        public List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages);

    }
}
