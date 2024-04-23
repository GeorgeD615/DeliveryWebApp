using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.ContainersForView
{
    public class OrderFormViewModel
    {
        public List<AddressViewModel>? Addresses { get; set; }
        public AddressViewModel? LastAddress { get; set; }
        public CartViewModel? Cart { get; set; }

        public OrderFormViewModel(List<AddressViewModel>? addresses, AddressViewModel? lastAddress, CartViewModel? cart) 
        {
            Addresses = addresses;
            LastAddress = lastAddress;
            Cart = cart;
        }
    }
}
