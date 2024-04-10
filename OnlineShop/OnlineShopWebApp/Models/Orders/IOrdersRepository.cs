namespace OnlineShopWebApp.Models.Orders
{
    public interface IOrdersRepository
    {
        List<Order> GetAll();
        void Add(Order order);
        Order? TryGetById(Guid orderId);
        void EditStatus(Guid orderId, StateOfOrder status);
    }
}
