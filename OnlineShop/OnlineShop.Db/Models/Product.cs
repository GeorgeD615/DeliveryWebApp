﻿namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string? Description { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
        public List<Favorite> UserProductFavorites { get; set; } = new();
        public List<Image> Images { get; set; } = new();
    }
}
