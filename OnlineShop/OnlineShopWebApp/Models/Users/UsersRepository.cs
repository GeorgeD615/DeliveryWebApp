namespace OnlineShopWebApp.Models.Users
{
    public static class UsersRepository
    {
        private static List<User> users = new();
        static UsersRepository()
        {
            users.Add(new User("Георгий"));
        }
    }
}
