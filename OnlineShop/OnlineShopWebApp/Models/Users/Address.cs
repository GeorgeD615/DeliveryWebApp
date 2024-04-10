using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Users
{
    public class Address
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Укажите название города")]
        [RegularExpression(@"[а-яА-я- ]{2,30}",
            ErrorMessage = "Название города должно иметь длину от 2 до 30 и состоять из символов кириллицы (а-я А-Я) и символа \"-\"")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите название улицы/проспекта")]
        [RegularExpression(@"[а-яА-я- ]{2,30}",
            ErrorMessage = "Название улицы/проспекта должно иметь длину до 30 и состоять из символов кириллицы (а-я А-Я) и символа \"-\"")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Укажите номер дома")]
        [RegularExpression(@"([0-9]+[0-9а-яА-Я/]*){1,7}",
            ErrorMessage = "Номер дома должен начинаться с цифр состоять из цифр, символов кириллицы (а-я А-Я) или символа / ")]
        public string House { get; set; }


        [Required(ErrorMessage = "Укажите номер квартиры")]
        [Range(0,10000, ErrorMessage = "Номер квартиры должен находится в диапозоне от 0 до 100000")]
        public int Flat { get; set; } 

        public Address()
        {
            Id = Guid.NewGuid();
        }

        [JsonConstructor]
        public Address(Guid id, Guid userId, string city, string street, string house, int flat)
        {
            Id = id;
            UserId = userId;
            City = city;
            Street = street;
            House = house;
            Flat = flat;
        }

        public override string ToString() => $"г. {City}, ул. {Street}, д. {House}, кв. {Flat}";
    }
}
