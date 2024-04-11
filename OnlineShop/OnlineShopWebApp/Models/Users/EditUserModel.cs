using OnlineShopWebApp.Models.Roles;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class EditUserModel
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public Role Role { get; set; }
    }
}