using Newtonsoft.Json;
using OnlineShopWebApp.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        [RegularExpression(@"[a-zA-Z0-9]{5,25}",
            ErrorMessage = "Логин должен иметь длину от 5 до 25 и состоять из символов латинского алфавита (a-z A-Z) и цифр(0-9)")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [RegularExpression(@"[a-zA-Z0-9/?#@<>.,!_]{5,30}",
            ErrorMessage = "Пароль должен иметь длину от 5 до 30 и состоять из символов латинского алфавита (a-z A-Z) и цифр(0-9) или символов / ? # @ < > . , ! _")]
        public string Password { get; set; }
        public List<ProductViewModel> Favorites { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
        public string Role { get; set; }
    }
}
