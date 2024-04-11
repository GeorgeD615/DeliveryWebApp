using OnlineShopWebApp.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class CreateUserViewModel
    {
        public RegistrationViewModel RegistrationModel { get; set; }
        public Guid RoleId { get; set; }
    }
}