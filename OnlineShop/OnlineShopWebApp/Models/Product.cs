namespace OnlineShopWebApp.Models
{
    public class Product
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; }
        public decimal Cost { get; }
        public string Description { get; }

        public Product(string name, decimal cost, string description)
        {
            Id = nextId++;
            Name = name;
            Cost = cost;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost} руб.";
        }
    }
}
