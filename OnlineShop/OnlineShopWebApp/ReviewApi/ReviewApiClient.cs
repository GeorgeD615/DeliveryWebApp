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
            try
            {
                var httpClient = httpClientFactory.CreateClient("ReviewApi");
                var reviews = await httpClient.GetFromJsonAsync<List<Review>>($"/Review/GetByProductId?productId={productId}");
                return reviews;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new List<Review>();
            }
        }
        public async Task CreateAsync(ReviewCreateModel review)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ReviewApi");
                await httpClient.PostAsJsonAsync($"/Review/Create", review);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return;
            }
        }
        public async Task DeleteReviewsByUserIdAsync(string userId)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ReviewApi");
                await httpClient.DeleteAsync($"/Review/DeleteByUserId?userId={userId}");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return;
            }
        }
    }
}
