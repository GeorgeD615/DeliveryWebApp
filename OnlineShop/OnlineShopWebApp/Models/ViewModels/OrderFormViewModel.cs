using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.ContainersForView
{
    public class OrderFormViewModel
    {
        public List<AddressViewModel>? Addresses { get; set; }
        public AddressViewModel? LastAddress { get; set; }
        public CartViewModel? Cart { get; set; }
        public string UserId { get; set; }

        public OrderFormViewModel(List<AddressViewModel>? addresses, AddressViewModel? lastAddress, CartViewModel? cart, string userId) 
        {
            Addresses = addresses;
            LastAddress = lastAddress;
            Cart = cart;
            UserId = userId;
        }
    }
}
