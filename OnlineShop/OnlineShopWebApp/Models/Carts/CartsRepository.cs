using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Carts
{
    public static class CartsRepository
    {
        private static List<Cart> carts = new();

        static CartsRepository()
        {
            foreach(var user in UsersRepository.GetAll())
            {
                carts.Add(new Cart(user.Id));
            }
        }
        public static void CreateCartForUser(int userId) => carts.Add(new Cart(userId));
        public static Cart TryGetByUserId(int id) => carts.FirstOrDefault(cart => cart.UserId == id);
        public static void AddProduct(Product product, int userId)
        {
            var cart = TryGetByUserId(userId);
            if (!cart.Items.Any(item => item.Product == product))
            {
                cart.Items.Add(new CartItem(product));
                return;
            }

            cart.Items.First(item => item.Product == product).Amount += 1;
        }
        public static int GetCartSize(int userId) => TryGetByUserId(userId).Items.Sum(item => item.Amount);
        public static void ClearCart(int userId) => TryGetByUserId(userId).Items.Clear();
    }
}
