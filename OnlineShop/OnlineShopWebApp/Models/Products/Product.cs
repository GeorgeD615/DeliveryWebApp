namespace OnlineShopWebApp.Models.Products
{
    public class Product
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }

        public Product(string name, decimal cost, string description)
        {
            Id = nextId++;
            Name = name;
            Cost = cost;
            Description = description;
        }

        public override string ToString() => $"{Id}\n{Name}\n{Cost} руб.";

    }
}
