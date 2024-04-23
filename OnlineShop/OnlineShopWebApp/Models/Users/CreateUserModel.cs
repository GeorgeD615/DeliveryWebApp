using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Models.Users
{
    public class CreateUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
