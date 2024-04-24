using OnlineShop.Db.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; }

        [ForeignKey("CartId")]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey("AddressId")]
        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string? CommentsToCourier { get; set; }
        public StateOfOrder StateOfOrder { get; set; }
        public DateTime TimeOfOrder { get; set; }
    }
}
