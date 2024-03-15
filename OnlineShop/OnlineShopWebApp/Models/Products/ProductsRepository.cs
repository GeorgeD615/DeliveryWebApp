namespace OnlineShopWebApp.Models.Products
{
    public static class ProductsRepository
    {
        private static List<Product> products { get; set; } = new()
        {
            {new Product("Эчпочмак", 90, "Очень вкусная булочка", "/images/products/eachpochmak.jpg")},
            {new Product("Хачапури", 125, "Очень вкусная лепёшка", "/images/products/hachapury.jpg")},
            {new Product("Хинкали", 300, "Очень вкусные пельмени", "/images/products/khinkali.jpg")},
            {new Product("Долма", 250, "Рулетики из виноградных листьев с фаршем внутри", "/images/products/dolma.jpg")},
            {new Product("Чак-чак", 200, "Традиционный татарский десерт", "/images/products/chuck-chuck.jpg")},
            {new Product("Самса", 90, "Узбекский пирожок", "/images/products/samsa.jpg")},
            {new Product("Нэмы", 300, "Вьетнамские фаршированные рулетики", "/images/products/nams.jpeg")},
            {new Product("Чахохбили", 400, "Рагу из птицы, тушенной с овощами, зеленью и специями", "/images/products/chahohbili.jpg")},
            {new Product("Суп «Харчо»", 350, "Вкусный, ароматный, острый", "/images/products/soup_harcho.jpg")},
            {new Product("Чихиртма", 250, "Густой грузинский суп", "/images/products/chihirtma.jpg")},
            {new Product("Кюфта", 230, "Армянские тефтели", "/images/products/kufta.jpg")},
            {new Product("Хаш", 450, "Горячий, очень сытный и жирный суп из говяжьих ног", "/images/products/hash.jpg")}
        };

        public static List<Product> GetAll() => products;
        public static int GetCount() => products.Count;
        public static Product? TryGetById(int productId) => products.FirstOrDefault(p => p.Id == productId);
        public static List<Product> GetPageOfProducts(int numOfProdPerPage, int pageNum, int amountOfPages)
        {
            int lastIndex = pageNum < amountOfPages ? numOfProdPerPage : products.Count - numOfProdPerPage * (pageNum - 1);
            return products.GetRange((pageNum - 1) * numOfProdPerPage, lastIndex);
        }
    }
}
