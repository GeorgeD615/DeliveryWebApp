using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Models.Carts
{
    public class CartsRepository : ICartsRepository
    {
        private List<Cart> carts = new();
        public Cart? TryGetByUserId(int userId) => carts.FirstOrDefault(cart => cart.UserId == userId);
        public void AddProduct(Product product, int userId)
        {
            var cart = carts.FirstOrDefault(cart => cart.UserId == userId);
            
            if(cart == null)
            {
                cart = new Cart(userId);
                carts.Add(cart);
            }

            var itemInCart = cart.Items.FirstOrDefault(item => item.Product == product);

            if (itemInCart == null)
                cart.Items.Add(new CartItem(product));
            else
                itemInCart.Amount += 1;
        }
        public void ChangeProductAmount(int userId, int cartItemId, int difference)
        {
            var cart = TryGetByUserId(userId);
            var cartItem = cart?.Items.FirstOrDefault(x => x.Id == cartItemId);

            if(cartItem == null)
            {
                //залогировать ошибку : карточка товара не найдена
                return;
            }

            int newAmount = cartItem.Amount + difference;

            if(newAmount == 0)
                cart?.Items.Remove(cartItem);
            else
                cartItem.Amount = newAmount;

            if(cart?.Items.Count == 0)
                carts.Remove(cart);
        }
        public void ClearCart(int userId) => carts.RemoveAll(cart => cart.UserId == userId);
    }
}
