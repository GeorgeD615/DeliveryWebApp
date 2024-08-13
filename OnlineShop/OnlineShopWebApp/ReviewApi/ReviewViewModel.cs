namespace OnlineShopWebApp.ReviewApi
{
    public class ReviewViewModel
    {
        public string? UserName { get; set; }
        public string? Text { get; set; }
        public string? UserAvatarUrl { get; set; }
        public int Grade { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
