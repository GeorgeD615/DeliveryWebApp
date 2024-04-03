namespace OnlineShopWebApp.Models.Orders
{
    public interface IOrdersRepository
    {
        List<Order> GetAll();
        void AddOrder (Order order);
        Order? TryGetOrder(int orderId);
    }
}
