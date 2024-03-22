namespace OnlineShopWebApp.Models.Users
{
    public class Address
    {
        private static int nextId = 0;
        public int Id { get; }
        public int UserId { get; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; } 

        public Address(int userId, string city, string street, string house, string flat)
        {
            Id = ++nextId;
            UserId = userId;
            City = city;
            Street = street;
            House = house;
            Flat = flat;
        }

        public override string ToString() => $"г. {City}, ул. {Street}, д. {House}, кв. {Flat}";
    }
}
