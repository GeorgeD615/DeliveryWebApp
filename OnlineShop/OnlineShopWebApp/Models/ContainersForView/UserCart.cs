using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.ContainersForView
{
    public class UserCart
    {
        public User User { get; set; }
        public Cart Cart { get; set; }

        public UserCart(User user, Cart cart) 
        {
            User = user;
            Cart = cart;
        }
    }
}
