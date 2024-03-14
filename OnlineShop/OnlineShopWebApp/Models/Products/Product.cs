namespace OnlineShopWebApp.Models.Products
{
    public class Product
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public Product(string name, decimal cost, string description, string imagePath)
        {
            Id = nextId++;
            Name = name;
            Cost = cost;
            Description = description;
            ImagePath = imagePath;
        }
    }
}
