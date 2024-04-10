namespace OnlineShopWebApp.Models.Roles
{
    public class Role
    {
        public Guid Id { get; }
        public string Name { get; set; }

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
