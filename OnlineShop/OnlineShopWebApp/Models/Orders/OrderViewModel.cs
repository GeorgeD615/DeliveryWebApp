using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Orders
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public CartViewModel Cart { get; set; }
        public Address Address { get; set; }
        public User User { get; set; }
        public string CommentsToCourier { get; set; }
        public StateOfOrder StateOfOrder { get; set; }
        public DateTime TimeOfOrder { get; set; }
    }
}
