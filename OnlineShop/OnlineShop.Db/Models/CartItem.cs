namespace OnlineShop.Db.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public Guid? CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
