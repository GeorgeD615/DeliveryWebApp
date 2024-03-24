using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.ContainersForView
{
    public class OrderViewModel
    {
        public List<Address> Addresses { get; set; }
        public Address LastAddress { get; set; }
        public Cart Cart { get; set; }

        public OrderViewModel(List<Address> addresses, Address lastAddress, Cart cart) 
        {
            Addresses = addresses;
            LastAddress = lastAddress;
            Cart = cart;
        }
    }
}
