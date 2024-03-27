using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models
{
    public static class ModelConverter
    {
        public static ProductViewModel ConvertToProductViewModel(Product product) => new ProductViewModel()
                                                                                    {
                                                                                        Id = product.Id,
                                                                                        Name = product.Name,
                                                                                        Cost = product.Cost,
                                                                                        Description = product.Description,
                                                                                        ImagePath = product.ImagePath,
                                                                                    };
    }
}
