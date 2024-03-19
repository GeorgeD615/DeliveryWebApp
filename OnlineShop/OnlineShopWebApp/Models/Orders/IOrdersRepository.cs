namespace OnlineShopWebApp.Models.Orders
{
    public interface IOrdersRepository
    {
        void AddOrder (Order order);
        Order? TryGetOrder(int orderId);
    }
}
