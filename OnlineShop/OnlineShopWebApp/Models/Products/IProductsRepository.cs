namespace OnlineShopWebApp.Models.Products
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        int GetCount();
        Product? TryGetById(Guid productId);
        List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages);
        public void DeleteProduct(Guid productId);
        public void Edit(ProductViewModel productEditModel);
        public void Add(ProductViewModel productCreateModel);
        List<Product> SearchByName(string name);
    }
}
