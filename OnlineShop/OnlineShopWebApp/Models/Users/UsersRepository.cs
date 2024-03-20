namespace OnlineShopWebApp.Models.Users
{
    public class UsersRepository : IUserRepository
    {
        private List<User> users = new();
        public UsersRepository()
        {
            users.Add(new User("Георгий"));
        }
    }
}
