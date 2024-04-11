using Newtonsoft.Json;

namespace OnlineShopWebApp.Models.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Data\\Orders.json";

        private List<Order> orders;
        public OrdersRepository() 
        {
            using var reader = new StreamReader(dataJsonFilePath);
            orders = JsonConvert.DeserializeObject<List<Order>>(reader.ReadToEnd());
        }

        public void Add(Order order) {
            orders.Add(order);
            SaveOrdersIntoJson();
        }

        public void EditStatus(Guid orderId, StateOfOrder status)
        {
            var order = TryGetById(orderId);

            if (order == null)
                return;
                
            order.StateOfOrder = status;
            SaveOrdersIntoJson();
        }

        public List<Order> GetAll() => orders;

        public Order? TryGetById(Guid orderId) => orders.FirstOrDefault(order => order.Id == orderId);
        private void SaveOrdersIntoJson()
        {
            using var writer = new StreamWriter(dataJsonFilePath, false);
            writer.Write(JsonConvert.SerializeObject(orders, Formatting.Indented));
        }
    }
}
