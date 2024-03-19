namespace OnlineShopWebApp.Models.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private List<Product> products = new()
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

        public List<Product> GetAll() => products;
        public int GetCount() => products.Count;
        public Product? TryGetById(int productId) => products.FirstOrDefault(p => p.Id == productId);
        public List<Product> GetPageOfProducts(int numberOfProductsPerPage, int pageNumber, int amountOfPages)
        {
            int lastIndex = pageNumber < amountOfPages ? numberOfProductsPerPage : products.Count - numberOfProductsPerPage * (pageNumber - 1);
            return products.GetRange((pageNumber - 1) * numberOfProductsPerPage, lastIndex);
        }
    }
}
