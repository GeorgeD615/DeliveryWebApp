using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Models.Users
{
    public class CreateUserViewModel
    {
        public RegistrationViewModel RegistrationModel { get; set; }
        public string RoleId { get; set; }
    }
}