namespace OnlineShopWebApp.Models.Roles
{
    public class Role
    {
        private int nextId = 0;
        public int Id { get; set; }
        public string Name { get; set; }

        public Role(string name)
        {
            Id = ++nextId;
            Name = name;
        }
    }
}
