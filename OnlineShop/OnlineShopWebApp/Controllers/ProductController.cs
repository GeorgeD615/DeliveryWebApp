using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public ProductController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index(int productId) => View(productsRepository.TryGetById(productId));

        public IActionResult Page(int numberOfProductsPerPage, int pageNumber)
        {
            if (numberOfProductsPerPage <= 0 || pageNumber <= 0)
                return View(null);

            int amountOfPages = productsRepository.GetCount() / numberOfProductsPerPage +
                ((productsRepository.GetCount() % numberOfProductsPerPage) == 0 ? 0 : 1);

            if (pageNumber > amountOfPages)
                return View(null);

            var productsPage = new ProductsPage()
            {
                AmountOfPages = amountOfPages,
                NumOfProdPerPage = numberOfProductsPerPage,
                PageNumber = pageNumber
            };

            switch (numberOfProductsPerPage)
            {
                case 3:
                    productsPage.CardSize = 3;
                    productsPage.ImageHeight = 200;
                    break;
                case 5:
                case 10:
                    productsPage.CardSize = 2;
                    productsPage.ImageHeight = 125;
                    break;
            }

            productsPage.Products = productsRepository.GetPageOfProducts(numberOfProductsPerPage, pageNumber, amountOfPages);

            return View(productsPage);
        }
    }
}
