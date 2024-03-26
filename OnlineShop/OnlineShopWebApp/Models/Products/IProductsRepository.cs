namespace OnlineShopWebApp.Models.Products
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        int GetCount();
        Product? TryGetById(int productId);
        List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages);
        public void DeleteProduct(int productId);
        public void EditProduct(ProductViewModel productEditModel);
        public void AddProduct(ProductViewModel productCreateModel);
    }
}
