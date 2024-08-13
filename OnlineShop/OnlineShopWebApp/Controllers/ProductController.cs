using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.ReviewApi;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly UserManager<User> userManager;
        private readonly IImagesRepository imagesRepository;
        private readonly ReviewApiClient reviewApiClient;

        public ProductController(IProductsRepository productsRepository, ReviewApiClient reviewApiClient, 
            UserManager<User> userManager, IImagesRepository imagesRepository)
        {
            this.productsRepository = productsRepository;
            this.reviewApiClient = reviewApiClient;
            this.userManager = userManager;
            this.imagesRepository = imagesRepository;
        }

        public async Task<ActionResult> Index(Guid productId)
        {
            var reviews = await reviewApiClient.GetReviewsAsync(productId);
            var product = await productsRepository.TryGetByIdAsync(productId);

            if (product == null)
                return View("Error");

            if(reviews.IsNullOrEmpty())
                return View(product.ToProductViewModel(new List<ReviewViewModel>()));

            var reviewsViewModels = new List<ReviewViewModel>(reviews!.Count);
            for (int i = 0; i < reviews.Count; ++i)
            {
                var user = await userManager.FindByIdAsync(reviews[i].UserId);

                if (user == null)
                    continue;

                var avatarUrl = (await imagesRepository.TryGetAvatarByUserIdAsync(user.Id))?.Url;
                avatarUrl ??= Constants.DefaultUserImagePath;
                reviewsViewModels.Add(reviews[i].ToReviewViewModel(user.UserName, avatarUrl));
            }

            return View(product.ToProductViewModel(reviewsViewModels));
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

            var productsViewModels = new List<ProductViewModel>(products.Count);

            for(int i = 0; i < products.Count; ++i)
            {
                var reviews = await reviewApiClient.GetReviewsAsync(products[i].Id);
                var reviewsViewModels = reviews.Select(r => r.ToReviewViewModel(null, null)).ToList();

                var productViewModel = products[i].ToProductViewModel(reviewsViewModels);

                productsViewModels.Add(productViewModel);
            }

            productsPage.Products = productsViewModels;

            return View(productsPage);
        }


        [HttpPost]
        public async Task<ActionResult> CreateReviewAsync(ProductViewModel productReviewModel)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var review = new ReviewCreateModel()
            {
                UserId = user.Id,
                ProductId = productReviewModel.Id,
                Grade = productReviewModel.NewReviewGrade ??= 5,
                Text = productReviewModel.NewReviewText
            };
            await reviewApiClient.CreateAsync(review);
            return RedirectToAction(nameof(Index), new { productId = review.ProductId });
        }
    }
}
