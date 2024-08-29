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
            var reviews = await httpClient.GetFromJsonAsync<List<Review>>($"/Review/GetByProductId?productId={productId}");
            return reviews;
        }
        public async Task CreateAsync(ReviewCreateModel review)
        {
            var httpClient = httpClientFactory.CreateClient("ReviewApi");
            await httpClient.PostAsJsonAsync($"/Review/Create", review);
        }
        public async Task DeleteReviewsByUserIdAsync(string userId)
        {
            var httpClient = httpClientFactory.CreateClient("ReviewApi");
            await httpClient.DeleteAsync($"/Review/DeleteByUserId?userId={userId}");
        }
    }
}
