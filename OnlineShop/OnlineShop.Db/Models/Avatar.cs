namespace OnlineShop.Db.Models
{
    public class Avatar
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
