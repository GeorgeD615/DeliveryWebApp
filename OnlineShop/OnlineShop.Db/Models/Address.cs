namespace OnlineShop.Db.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int Flat { get; set; }
        public bool IsLast { get; set; }
        public User User { get; set; }
    }
}
