using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Users
{
    public class Address
    {
        private static int nextId = 0;
        public int Id { get; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Укажите название города")]
        [RegularExpression(@"[а-яА-я- ]{1,30}", 
            ErrorMessage = "Название города должно иметь длину до 30 и состоять из символов кириллицы (а-я А-Я) и символа \"-\"")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите название улицы/проспекта")]
        [RegularExpression(@"[а-яА-я- ]{1,30}",
            ErrorMessage = "Название улицы/проспекта должно иметь длину до 30 и состоять из символов кириллицы (а-я А-Я) и символа \"-\"")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Укажите номер дома")]
        public string House { get; set; }


        [Required(ErrorMessage = "Укажите номер квартиры")]
        public string Flat { get; set; } 

        public Address()
        {
            Id = ++nextId;
        }

        public override string ToString() => $"г. {City}, ул. {Street}, д. {House}, кв. {Flat}";
    }
}
