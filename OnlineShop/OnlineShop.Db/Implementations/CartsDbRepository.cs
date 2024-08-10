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
        public async Task<Cart?> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Carts
                .Include(cart => cart.Items)
                .ThenInclude(item => item.Product)
                .ThenInclude(product => product.Images)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task AddProductAsync(Product product, string userId)
        {
            var cart = await TryGetByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart() { UserId = userId.ToString() };
                databaseContext.Carts.Add(cart);
            }

            var itemInCart = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id);

            if (itemInCart == null)
            {
                var newCartItem = new CartItem()
                {
                    ProductId = product.Id,
                    Amount = 1,
                    CartId = cart.Id
                };
                cart.Items.Add(newCartItem);
                databaseContext.CartItems.Add(newCartItem);

            }
            else
                itemInCart.Amount += 1;

            await databaseContext.SaveChangesAsync();
        }
        public async Task ChangeProductAmountAsync(string userId, Guid cartItemId, int difference)
        {
            var cart = await TryGetByUserIdAsync(userId);

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

            await databaseContext.SaveChangesAsync();
        }
        public async Task ClearCartByUserIdAsync(string userId)
        {
            var cart = await TryGetByUserIdAsync(userId);

            if (cart == null)
                throw new Exception("Корзина не найдена");

            databaseContext.Carts.Remove(cart);
            await databaseContext.SaveChangesAsync();
        }
    }
}
