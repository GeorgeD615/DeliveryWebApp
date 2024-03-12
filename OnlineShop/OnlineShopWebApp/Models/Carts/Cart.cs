using OnlineShopWebApp.Models.Products;
using System.Globalization;

namespace OnlineShopWebApp.Models.Carts
{
    public class Cart
    {
        private static int nextId = 0;
        public int Id { get; }
        public int UserId { get; }

        public List<CartItem> Items = new();
        public string Cost { get => Items.Sum(item => item.Cost).ToString("#,#", new CultureInfo("ru-RU"));}
        public Cart(int userId)
        {
            Id = ++nextId;
            UserId = userId;
        }
    }
}
