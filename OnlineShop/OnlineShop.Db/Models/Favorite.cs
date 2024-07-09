using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
