using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Roles
{
    public class RoleCreateFormViewModel
    {
        public List<Role> roles;

        [Required(ErrorMessage = "Укажите название роли")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Название роли должно содержать от 2 до 15 символов")]
        public string Name { get; set; }
    }
}
