using Newtonsoft.Json;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Orders
{
    public class Order
    {
        public Guid Id { get; }
        public Cart Cart { get; set; }
        public Address Address { get; set; }
        public User User { get; set; }
        public string CommentsToCourier { get; set; }
        public StateOfOrder StateOfOrder { get; set; }
        public DateTime TimeOfOrder { get; }
        public Order(Cart cart, Address address, string commentsToCourier, User user)
        {
            Id = Guid.NewGuid();
            Cart = cart;
            Address = address;
            CommentsToCourier = commentsToCourier;
            StateOfOrder = StateOfOrder.InProcessing;
            TimeOfOrder = DateTime.Now;
            User = user;
        }

        [JsonConstructor]
        public Order(Guid id, Cart cart, Address address, User user, string commentsToCourier, StateOfOrder stateOfOrder, DateTime timeOfOrder)
        {
            Id = id;
            Cart = cart;
            Address = address;
            CommentsToCourier = commentsToCourier;
            StateOfOrder = stateOfOrder;
            TimeOfOrder = timeOfOrder;
            User = user;
        }
    }
}
