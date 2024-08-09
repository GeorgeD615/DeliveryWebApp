using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public List<Address> Addresses { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Favorite> UserProductFavorites { get; set; } = new();
        public Avatar? Avatar { get; set; }
    }
}
