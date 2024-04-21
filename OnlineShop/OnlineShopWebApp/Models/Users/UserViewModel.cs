using Newtonsoft.Json;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Models.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<ProductViewModel> Favorites { get; set; }
        public List<Address> Addresses { get; set; }
        public Address? LastAddress { get; set; }
        public Role Role { get; set; }
    }
}
