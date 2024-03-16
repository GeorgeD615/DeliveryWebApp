using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int productId) => View(ProductsRepository.TryGetById(productId));

        public IActionResult Page(int numberOfProductsPerPage, int pageNumber)
        {
            if (numberOfProductsPerPage <= 0 || pageNumber <= 0)
                return View(null);

            int amountOfPages = ProductsRepository.GetCount() / numberOfProductsPerPage +
                ((ProductsRepository.GetCount() % numberOfProductsPerPage) == 0 ? 0 : 1);

            if (pageNumber > amountOfPages)
                return View(null);

            var productsPage = new ProductsPage()
            {
                AmountOfPages = amountOfPages,
                NumOfProdPerPage = numberOfProductsPerPage
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

            productsPage.Products = ProductsRepository.GetPageOfProducts(numberOfProductsPerPage, pageNumber, amountOfPages);

            return View(productsPage);
        }
    }
}
