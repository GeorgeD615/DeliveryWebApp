using Newtonsoft.Json;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Orders
{
    public class Order
    {
        public static int nextId = 0;
        public int Id { get; }
        public Cart Cart { get; set; }
        public Address Address { get; set; }
        public User User { get; set; }
        public string CommentsToCourier { get; set; }
        public StateOfOrder StateOfOrder { get; set; }
        public DateTime TimeOfOrder { get; }
        public Order(Cart cart, Address address, string commentsToCourier, User user)
        {
            Id = ++nextId;
            Cart = cart;
            Address = address;
            CommentsToCourier = commentsToCourier;
            StateOfOrder = StateOfOrder.InProcessing;
            TimeOfOrder = DateTime.Now;
            User = user;
        }

        [JsonConstructor]
        public Order(Cart cart, Address address, User user, string commentsToCourier, StateOfOrder stateOfOrder, DateTime timeOfOrder)
            : this(cart, address, commentsToCourier, user)
        {
            StateOfOrder = stateOfOrder;
            TimeOfOrder = timeOfOrder;
        }
    }
}
