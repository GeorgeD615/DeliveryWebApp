using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Carts
{
    public static class CartsRepository
    {
        private static List<Cart> carts = new List<Cart>();

        static CartsRepository()
        {
            foreach(var user in UsersRepository.GetAll())
            {
                carts.Add(new Cart(user.Id));
            }
        }

        public static List<Cart> GetAll() => carts;
        public static Cart? TryGetByUserId(int id) => carts.FirstOrDefault(cart => cart.UserId == id);
        public static void AddProductToUserCart(Product product, int userId)
        {
            TryGetByUserId(userId)?.AddProduct(product);
        }
    }
}
