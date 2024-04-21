using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.ContainersForView
{
    public class OrderFormViewModel
    {
        public List<Address>? Addresses { get; set; }
        public Address? LastAddress { get; set; }
        public CartViewModel? Cart { get; set; }

        public OrderFormViewModel(List<Address>? addresses, Address? lastAddress, CartViewModel? cart) 
        {
            Addresses = addresses;
            LastAddress = lastAddress;
            Cart = cart;
        }
    }
}
