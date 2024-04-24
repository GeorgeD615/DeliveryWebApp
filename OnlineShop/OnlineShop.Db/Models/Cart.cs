namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public bool IsOrdered { get; set; }
        public Order Order { get; set; }
    }
}
