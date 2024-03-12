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

        public static Cart? TryGetByUserId(int id) => carts.FirstOrDefault(cart => cart.UserId == id);
        public static void AddProductToCart(Product product, int userId) => TryGetByUserId(userId)?.AddProduct(product);
    }
}
