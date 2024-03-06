using Newtonsoft.Json;
using System.IO;

namespace OnlineShopWebApp.Models
{
    public static class ProductsRepository
    {
        public static List<Product> Products { get; set; } = new List<Product>()
        {
            {new Product("Эчпочмак", 90, "Очень вкусная булочка")},
            {new Product("Хачапури", 125, "Очень вкусная лепёшка")},
            {new Product("Хинкали", 300, "Очень вкусные пельмени")},
            {new Product("Долма", 250, "Рулетики из виноградных листьев с фаршем внутри")},
            {new Product("Чак-чак", 200, "Традиционный татарский десерт")},
            {new Product("Самса", 90, "Узбекский пирожок")},
            {new Product("Нэмы", 300, "Вьетнамские фаршированные рулетики")},
            {new Product("Чахохбили", 400, "Рагу из птицы, тушенной с овощами, зеленью и специями")},
            {new Product("Суп «Харчо»", 350, "Вкусный, ароматный, острый")},
            {new Product("Чихиртма", 250, "Густой грузинский суп")},
            {new Product("Кюфта", 230, "Армянские тефтели")},
            {new Product("Хаш", 450, "Горячий, очень сытный и жирный суп из говяжьих ног")}
        };

        public static Product? GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public static List<Product> GetPageOfProducts(int productsNum, int pageNum, int pages)
        {
            return pageNum < pages ?
                Products.GetRange((pageNum - 1) * productsNum, productsNum) :
                Products.GetRange((pageNum - 1) * productsNum, Products.Count - productsNum * (pageNum - 1));
        }
    }
}
