using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class EditUserViewModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        [RegularExpression(@"[a-zA-Z0-9]{5,25}",
            ErrorMessage = "Логин должен иметь длину от 5 до 25 и состоять из символов латинского алфавита (a-z A-Z) и цифр(0-9)")]
        public string Login { get; set; }
        public Guid? RoleId { get; set; }
    }
}