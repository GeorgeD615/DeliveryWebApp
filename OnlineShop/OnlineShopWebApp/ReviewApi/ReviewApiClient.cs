using OnlineShop.Db.Models;
using System.Net.Http.Json;

namespace OnlineShopWebApp.ReviewApi
{
    public class ReviewApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ReviewApiClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<Review>?> GetReviewsAsync(Guid productId)
        {
            var httpClient = httpClientFactory.CreateClient("ReviewApi");

            if (httpClient == null)
                return null;

            var reviews = await httpClient.GetFromJsonAsync<List<Review>>($"/Review/GetByProductId?productId={productId}");
            return reviews;
        }
        public async Task CreateAsync(ReviewCreateModel review)
        {
            var httpClient = httpClientFactory.CreateClient("ReviewApi");

            if (httpClient == null)
                return;

            await httpClient.PostAsJsonAsync($"Review/Create", review);
        }
    }
}
