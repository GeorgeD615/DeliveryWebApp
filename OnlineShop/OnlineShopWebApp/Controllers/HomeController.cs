using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.ReviewApi;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ReviewApiClient reviewApiClient;

        public HomeController(IProductsRepository productsRepository, ReviewApiClient reviewApiClient)
        {
            this.productsRepository = productsRepository;
            this.reviewApiClient = reviewApiClient;
        }

        public async Task<ActionResult> Index(string name) {
            var searchName = name?.Trim();

            var products = string.IsNullOrEmpty(searchName) ? 
                await productsRepository.GetAllAsync() : 
                await productsRepository.SearchByNameAsync(searchName);

            var productsViewModels = new List<ProductViewModel>(products.Count);

            for (int i = 0; i < products.Count; ++i)
            {
                var reviews = await reviewApiClient.GetReviewsAsync(products[i].Id);
                var reviewsViewModels = reviews.Select(r => r.ToReviewViewModel(null, null)).ToList();

                var productViewModel = products[i].ToProductViewModel(reviewsViewModels);

                productsViewModels.Add(productViewModel);
            }

            return View(productsViewModels);
        }
    }
}
