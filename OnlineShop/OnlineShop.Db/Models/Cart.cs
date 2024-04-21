namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }
}
