namespace OnlineShopWebApp.Models.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private List<Order> orders = new();
        public void AddOrder(Order order) => orders.Add(order);
        public Order? TryGetOrder(int orderId) => orders.FirstOrDefault(order => order.Id == orderId);
    }
}
