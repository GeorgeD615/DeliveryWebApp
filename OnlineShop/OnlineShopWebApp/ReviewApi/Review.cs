namespace OnlineShopWebApp.ReviewApi
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string UserId { get; set; }

        public string? Text { get; set; }

        public int Grade { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
