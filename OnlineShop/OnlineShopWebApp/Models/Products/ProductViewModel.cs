using Microsoft.IdentityModel.Tokens;
using OnlineShop.Db;
using OnlineShopWebApp.ReviewApi;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Products
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Укажите название блюда")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Название блюда должно иметь длину от 2 до 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите стоимость блюда")]
        [Range(1, 10000, ErrorMessage = "Стоимость блюда должна быть в диапозоне от 1 до 10000")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Укажите описание блюда")]
        [StringLength(1000, ErrorMessage = "Длина описания не должна превышать 1000 символов")]
        public string Description { get; set; }

        public string[] ImagesPaths { get; set; }

        public string ImagePath => ImagesPaths.IsNullOrEmpty() ? Constants.DefaultProductImagePath : ImagesPaths[0];

        public List<ReviewViewModel>? Reviews { get; set; }
        public double? Grade { get; }
        public string? NewReviewText { get; set; }
        public int? NewReviewGrade { get; set; }

        public ProductViewModel(Guid id, string name, decimal cost, string description, string[] imagesPaths, List<ReviewViewModel>? reviews = null)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            ImagesPaths = imagesPaths;
            Reviews = reviews;
            Grade = reviews.IsNullOrEmpty() ? null : reviews.Select(r => r.Grade).Average();
        }

        public ProductViewModel() { }

    }
}
