using System.Globalization;
using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Carts
{
    public class Cart
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public List<CartItem> Items { get; } = new();
        public string Cost { get => Items.Sum(item => item.Cost).ToString("#,#", new CultureInfo("ru-RU"));}
        public int Amount { get => Items.Sum(item => item.Amount); }
        public Cart(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }

        [JsonConstructor]
        public Cart(Guid id, Guid userId, List<CartItem> items)
        {
            Id = id;
            UserId = userId;
            Items = items;
        }
    }
}
