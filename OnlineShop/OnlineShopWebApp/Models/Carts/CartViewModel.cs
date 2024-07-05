using System.Globalization;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartViewModel
    {
        public Guid Id { get; }
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
        public string Cost { get => Items.Sum(item => item.Cost).ToString("#,#", new CultureInfo("ru-RU"));}
        public int Amount { get => Items.Sum(item => item.Amount); }

        public CartViewModel(Guid id, string userId, List<CartItemViewModel> items)
        {
            Id = id;
            UserId = userId;
            Items = items;
        }

        public CartViewModel() { }
    }
}
