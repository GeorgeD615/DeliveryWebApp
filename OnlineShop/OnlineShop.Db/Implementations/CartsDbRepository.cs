using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Implementations
{
    public class CartsDbRepository : ICartsRepository
    {
        private readonly DatabaseContext databaseContext;
        public CartsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public Cart? TryGetByUserId(Guid userId)
        {
            return databaseContext.Carts
                .Include(cart => cart.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(cart => cart.UserId == userId);
        }

        public void AddProduct(Product product, Guid userId)
        {
            var cart = TryGetByUserId(userId);

            if (cart == null)
            {
                cart = new Cart() { UserId = userId };
                databaseContext.Carts.Add(cart);
            }

            var itemInCart = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id);

            if (itemInCart == null)
                cart.Items.Add(new CartItem() { Product = product, Amount = 1, Cart = cart });
            else
                itemInCart.Amount += 1;

            databaseContext.SaveChanges();
        }
        public void ChangeProductAmount(Guid userId, Guid cartItemId, int difference)
        {
            var cart = TryGetByUserId(userId);

            if(cart == null) 
                throw new Exception("Корзина не найдена");

            var cartItem = cart.Items.FirstOrDefault(x => x.Id == cartItemId);

            if (cartItem == null)
                throw new Exception("CartItem не найден");

            int newAmount = cartItem.Amount + difference;

            if (newAmount == 0)
                databaseContext.CartItems.Remove(cartItem);
            else
                cartItem.Amount = newAmount;

            if (!cart.Items.Any())
                databaseContext.Carts.Remove(cart);

            databaseContext.SaveChanges();
        }
        public void ClearCart(Guid userId)
        {
            var cart = TryGetByUserId(userId);

            if (cart == null)
                throw new Exception("Корзина не найдена");

            databaseContext.Carts.Remove(cart);
            databaseContext.SaveChanges();
        }
    }
}
