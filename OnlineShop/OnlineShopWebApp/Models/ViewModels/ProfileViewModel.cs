using Microsoft.IdentityModel.Tokens;
using OnlineShop.Db;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [RegularExpression(@"[a-zA-Z0-9]{5,25}",
            ErrorMessage = "Логин должен иметь длину от 5 до 25 и состоять из символов латинского алфавита (a-z A-Z) и цифр(0-9)")]
        public string Login { get; set; }
        public List<AddressViewModel>? Addresses { get; set; }
        public List<OrderViewModel>? Orders { get; set; }
        public IFormFile? UploadedFile { get; set; }
        public string AvatarImagesPath { get; set; }
    }
}
