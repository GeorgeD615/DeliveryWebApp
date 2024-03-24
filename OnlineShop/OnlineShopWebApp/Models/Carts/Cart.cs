﻿using System.Globalization;

namespace OnlineShopWebApp.Models.Carts
{
    public class Cart
    {
        private static int nextId = 0;
        public int Id { get; }
        public int UserId { get; }
        public List<CartItem> Items { get; } = new();
        public string Cost { get => Items.Sum(item => item.Cost).ToString("#,#", new CultureInfo("ru-RU"));}
        public int Amount { get => Items.Sum(item => item.Amount); }
        public Cart(int userId)
        {
            Id = ++nextId;
            UserId = userId;
        }
    }
}
