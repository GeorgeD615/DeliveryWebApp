using Newtonsoft.Json;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartsRepository : ICartsRepository
    {
        private static readonly string dataJsonFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Data\\Carts.json";

        private List<Cart> carts;

        public CartsRepository() 
        {
            using var reader = new StreamReader(dataJsonFilePath);
            carts = JsonConvert.DeserializeObject<List<Cart>>(reader.ReadToEnd());
        }
        public Cart? TryGetByUserId(Guid userId) => carts.FirstOrDefault(cart => cart.UserId == userId);
        public void AddProduct(Product product, Guid userId)
        {
            var cart = carts.FirstOrDefault(cart => cart.UserId == userId);
            
            if(cart == null)
            {
                cart = new Cart(userId);
                carts.Add(cart);
            }

            var itemInCart = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id);

            if (itemInCart == null)
                cart.Items.Add(new CartItem(product));
            else
                itemInCart.Amount += 1;

            SaveCartsIntoJson();
        }
        public void ChangeProductAmount(Guid userId, Guid cartItemId, int difference)
        {
            var cart = TryGetByUserId(userId);
            var cartItem = cart?.Items.FirstOrDefault(x => x.Id == cartItemId);

            if(cartItem == null)
            {
                return;
            }

            int newAmount = cartItem.Amount + difference;

            if(newAmount == 0)
                cart?.Items.Remove(cartItem);
            else
                cartItem.Amount = newAmount;

            if(cart?.Items.Count == 0)
                carts.Remove(cart);

            SaveCartsIntoJson();
        }
        public void ClearCart(Guid userId)
        {
            carts.RemoveAll(cart => cart.UserId == userId);
            SaveCartsIntoJson();
        }

        private void SaveCartsIntoJson()
        {
            using var writer = new StreamWriter(dataJsonFilePath, false);
            writer.Write(JsonConvert.SerializeObject(carts, Formatting.Indented));
        }
    }
}
