using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Products
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public Product(string name, decimal cost, string description, string imagePath)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cost = cost;
            Description = description;
            ImagePath = imagePath;
        }

        [JsonConstructor]
        public Product(Guid id, string name, decimal cost, string description, string imagePath)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            ImagePath= imagePath;
        }
    }
}
