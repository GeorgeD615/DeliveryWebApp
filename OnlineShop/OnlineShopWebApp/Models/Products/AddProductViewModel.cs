using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Products
{
    public class AddProductViewModel
    {

        [Required(ErrorMessage = "Укажите название блюда")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Название блюда должно иметь длину от 2 до 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите стоимость блюда")]
        [Range(1, 10000, ErrorMessage = "Стоимость блюда должна быть в диапозоне от 1 до 10000")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Укажите описание блюда")]
        [StringLength(1000, ErrorMessage = "Длина описания не должна превышать 1000 символов")]
        public string Description { get; set; }

        public IFormFile[] UploadedFiles { get; set; }
    }
}
