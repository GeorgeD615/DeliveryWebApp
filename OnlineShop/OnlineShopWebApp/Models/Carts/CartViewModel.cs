using System.Globalization;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
        public string Cost { get => Items.Sum(item => item.Cost).ToString("#,#", new CultureInfo("ru-RU"));}
        public int Amount { get => Items.Sum(item => item.Amount); }
    }
}
