namespace OnlineShopWebApp.Models.Users
{
    public class UsersRepository : IUserRepository
    {
        private List<User> users = new();
        public UsersRepository()
        {
            users.Add(new User("Георгий"));
        }
        public void AddCard(int userId, string cardNumber) => users.FirstOrDefault(user => user.Id == userId)?.Cards.Add(cardNumber);
    }
}
