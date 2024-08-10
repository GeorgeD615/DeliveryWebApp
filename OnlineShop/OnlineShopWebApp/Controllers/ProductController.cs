using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;
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

        public async Task<ActionResult> Index(Guid productId)
        {
            var product = await productsRepository.TryGetByIdAsync(productId);

            if (product == null)
                return View("Error");

            return View(product.ToProductViewModel());
        }

        public async Task<ActionResult> PageAsync(int numberOfProductsPerPage, int pageNumber)
        {
            if (numberOfProductsPerPage <= 0 || pageNumber <= 0)
                return View(null);

            int amountOfPages = await productsRepository.GetAmountAsync() / numberOfProductsPerPage +
                ((await productsRepository.GetAmountAsync() % numberOfProductsPerPage) == 0 ? 0 : 1);

            if (pageNumber > amountOfPages)
                return View(null);

            var productsPage = new ProductsPageViewModel()
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
                
            var products = await productsRepository.GetPageOfProductsAsync(numberOfProductsPerPage, pageNumber, amountOfPages);
            productsPage.Products = products.Select(product => product.ToProductViewModel()).ToList();  

            return View(productsPage);
        }
    }
}
