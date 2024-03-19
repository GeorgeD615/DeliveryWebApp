using OnlineShopWebApp.Models.Carts;

namespace OnlineShopWebApp.Models.Orders
{
    public class Order
    {
        public static int nextId = 0;
        public int Id { get; }
        public string Address { get; set; }
        public string CardNumber { get; set; }
        public bool IsDelevered { get; set; }
        public string Message { get; set; }
        public Cart Cart { get; set; }
        public Order(string address, string cardNumber, Cart cart, string message)
        {
            Id = ++nextId;
            Address = address;
            CardNumber = cardNumber;
            IsDelevered = false;
            Cart = cart;
            Message = message;
        }
    }
}
