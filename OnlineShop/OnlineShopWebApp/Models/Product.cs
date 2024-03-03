namespace OnlineShopWebApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }

        public Product(string name, double cost, string description)
        {
            Name = name;
            Cost = cost;
            Description = description;
        }
    }
}
