namespace OnlineShopWebApp.Models.Users
{
    public class User
    {
        private static int nextId = 0;
        public int Id { get; }
        public string Name { get; set; }
        public List<string> Cards { get; }

        public User(string name)
        {
            Id = ++nextId;
            Name = name;
            Cards = new List<string>();
        }
    }
}
