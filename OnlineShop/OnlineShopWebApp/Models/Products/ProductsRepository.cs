namespace OnlineShopWebApp.Models.Products
{
    public static class ProductsRepository
    {
        private static List<Product> Products { get; set; } = new List<Product>()
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

        public static List<Product> GetAll() => Products;
        public static int GetCount() => Products.Count;
        public static Product? TryGetById(int id) => Products.FirstOrDefault(p => p.Id == id);
        public static List<Product> GetPageOfProducts(int size, int count, int pages)
        {
            return count < pages ?
                Products.GetRange((count - 1) * size, size) :
                Products.GetRange((count - 1) * size, Products.Count - size * (count - 1));
        }
    }
}
