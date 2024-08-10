using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Enums;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models.Orders
{
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext databaseContext;
        public OrdersDbRepository(DatabaseContext databaseContext) 
        {
            this.databaseContext = databaseContext;
        }

        public async Task AddAsync(Order order) 
        {
            databaseContext.Orders.Add(order);
            await databaseContext.SaveChangesAsync();
        }

        public async Task EditStatusAsync(Guid orderId, StateOfOrder status)
        {
            var order = await TryGetByIdAsync(orderId);

            if (order == null)
                throw new Exception("Заказ не найден");
                
            order.StateOfOrder = status;
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync() 
        {
            return await databaseContext.Orders.AsNoTracking()
                .Include(order => order.CartItems)
                .ThenInclude(item => item.Product)
                .Include(order => order.User)
                .ToListAsync(); 
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await databaseContext.Orders
                .Include(order => order.CartItems)
                .ThenInclude(item => item.Product)
                .Include(order => order.Address)
                .Where(order => order.UserId == userId).ToListAsync();
        }

        public async Task<Order?> TryGetByIdAsync(Guid orderId)
        {
            return await databaseContext.Orders
                .Include(order => order.CartItems)
                .ThenInclude(item => item.Product)
                .Include(order => order.User)
                .Include(order => order.Address)
                .FirstOrDefaultAsync(order => order.Id == orderId);
        }
    }
}
