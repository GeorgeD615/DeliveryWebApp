using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Db.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Product> Favorites { get; set; } = new();
        public List<Address> Addresses { get; set; }

        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
