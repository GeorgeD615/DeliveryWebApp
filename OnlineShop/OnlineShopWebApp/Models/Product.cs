namespace OnlineShopWebApp.Models
{
    public class Product
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }

        public Product(string name, double cost, string description)
        {
            Id = nextId++;
            Name = name;
            Cost = cost;
            Description = description;
        }
    }
}
