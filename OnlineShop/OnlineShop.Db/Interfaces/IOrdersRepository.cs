using OnlineShop.Db.Enums;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models.Orders
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task<Order?> TryGetByIdAsync(Guid orderId);
        Task EditStatusAsync(Guid orderId, StateOfOrder status);
        Task<List<Order>> GetByUserIdAsync(string userId);
    }
}
