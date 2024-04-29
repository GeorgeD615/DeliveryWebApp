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

        public void Add(Order order) 
        {
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
        }

        public void EditStatus(Guid orderId, StateOfOrder status)
        {
            var order = TryGetById(orderId);

            if (order == null)
                throw new Exception("Заказ не найден");
                
            order.StateOfOrder = status;
            databaseContext.SaveChanges();
        }

        public List<Order> GetAll() 
        {
            return databaseContext.Orders.AsNoTracking()
                .Include(order => order.CartItems)
                .ThenInclude(item => item.Product)
                .Include(order => order.User)
                .ToList(); 
        }

        public Order? TryGetById(Guid orderId)
        {
            return databaseContext.Orders
                .Include(order => order.CartItems)
                .ThenInclude(item => item.Product)
                .Include(order => order.User)
                .Include(order => order.Address)
                .FirstOrDefault(order => order.Id == orderId);
        }
    }
}
