using OnlineShopWebApp.Models.Roles;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class CreateUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public Role Role { get; set; }
    }
}
