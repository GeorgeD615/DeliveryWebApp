using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private static readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\OrdersData.json";

        private List<Order> orders;
        public OrdersRepository() 
        {
            using var reader = new StreamReader(dataJsonFilePath);
            orders = JsonConvert.DeserializeObject<List<Order>>(reader.ReadToEnd());
        }

        public void AddOrder(Order order) {
            orders.Add(order);
            SaveOrdersIntoJson();
        }
        public Order? TryGetOrder(int orderId) => orders.FirstOrDefault(order => order.Id == orderId);
        private void SaveOrdersIntoJson()
        {
            using var writer = new StreamWriter(dataJsonFilePath, false);
            writer.Write(JsonConvert.SerializeObject(orders, Formatting.Indented));
        }
    }
}
