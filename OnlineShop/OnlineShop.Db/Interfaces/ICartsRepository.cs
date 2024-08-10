using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart?> TryGetByUserIdAsync(string userId);
        Task AddProductAsync(Product product, string userId);
        Task ChangeProductAmountAsync(string userId, Guid cartItemId, int difference);
        Task ClearCartByUserIdAsync(string userId);
    }
}
