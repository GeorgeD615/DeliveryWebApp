namespace OnlineShopWebApp.Models.Users
{
    public static class UsersRepository
    {
        private static List<User> users = new List<User>();
        static UsersRepository()
        {
            users.Add(new User("Георгий"));
        }
        public static List<User> GetAll() => users;
        public static User? TryGetById(int id) => users.FirstOrDefault(u => u.Id == id);
    }
}
