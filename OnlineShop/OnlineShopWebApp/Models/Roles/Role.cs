namespace OnlineShopWebApp.Models.Roles
{
    public class Role
    {
        private static int nextId = 0;
        public int Id { get; }
        public string Name { get; set; }

        public Role(string name)
        {
            Id = ++nextId;
            Name = name;
        }
    }
}
