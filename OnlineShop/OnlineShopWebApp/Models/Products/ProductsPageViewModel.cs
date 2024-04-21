namespace OnlineShopWebApp.Models.Products
{
    public class ProductsPageViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int AmountOfPages { get; set; }
        public int PageNumber { get; set; }
        public int NumOfProdPerPage { get; set; }
        public int CardSize { get; set; }
        public int ImageHeight { get; set; }
    }
}
