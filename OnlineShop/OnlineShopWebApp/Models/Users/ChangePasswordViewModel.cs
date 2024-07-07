 using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [RegularExpression(@"[a-zA-Z0-9/?#@<>.,!_]{5,30}",
            ErrorMessage = "Пароль должен иметь длину от 5 до 30 и состоять из символов латинского алфавита (a-z A-Z) и цифр(0-9) или символов / ? # @ < > . , ! _")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}