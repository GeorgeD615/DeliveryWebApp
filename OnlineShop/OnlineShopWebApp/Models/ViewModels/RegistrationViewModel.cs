using System.ComponentModel.DataAnnotations;
namespace OnlineShopWebApp.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Укажите login")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Длина логина должна быть от 5 до 25 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть от 5 до 30 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
