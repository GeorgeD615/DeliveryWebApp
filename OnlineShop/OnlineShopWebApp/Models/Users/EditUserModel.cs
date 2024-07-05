using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Models.Users
{
    public class EditUserModel
    {
        public string UserId { get; set; }
        public string Login { get; set; }
        public RoleViewModel Role { get; set; }
    }
}