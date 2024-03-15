using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int productId) => View(ProductsRepository.TryGetById(productId));

        public IActionResult Page(int numOfProdPerPage, int pageNum)
        {
            if (numOfProdPerPage <= 0 || pageNum <= 0)
                return View(null);

            int amountOfPages = ProductsRepository.GetCount() / numOfProdPerPage +
                ((ProductsRepository.GetCount() % numOfProdPerPage) == 0 ? 0 : 1);

            if (pageNum > amountOfPages)
                return View(null);

            var productsPage = new ProductsPage()
            {
                AmountOfPages = amountOfPages,
                NumOfProdPerPage = numOfProdPerPage
            };

            switch (numOfProdPerPage)
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

            productsPage.Products = ProductsRepository.GetPageOfProducts(numOfProdPerPage, pageNum, amountOfPages);

            return View(productsPage);
        }
    }
}
