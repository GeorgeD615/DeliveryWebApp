using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public List<Address> Addresses { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<UserProductFavorite> UserProductFavorites { get; set; } = new();
    }
}
