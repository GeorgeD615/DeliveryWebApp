using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Helpers
{
    public static class ModelConverter
    {
        public static ProductViewModel ConvertToProductViewModel(Product product) => new ProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            Cost = product.Cost,
            Description = product.Description,
            ImagePath = product.ImagePath,
        };

        public static Product ConvertToProduct(ProductViewModel productViewModel) => new Product()
        {
            Name = productViewModel.Name,
            Cost = productViewModel.Cost,
            Description = productViewModel.Description,
            ImagePath = productViewModel.ImagePath
        };

        public static CartItemViewModel ConvertToCartItemViewModel(CartItem cartItem) => new CartItemViewModel()
        {
            Id = cartItem.Id,
            Product = ConvertToProductViewModel(cartItem.Product),
            Amount = cartItem.Amount
        };

        public static CartViewModel ConvertToCartViewModel(Cart cart) => new CartViewModel()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = cart.Items.Select(ConvertToCartItemViewModel).ToList()
        };

        public static AddressViewModel ConvertToAddressViewModel(Address address) => new AddressViewModel()
        {
            Id = address.Id,
            UserId = address.UserId,
            City = address.City,
            Street = address.Street,
            House = address.House,
            Flat = address.Flat
        };

        public static OrderViewModel ConvertToOrderViewModel(Order order) => new OrderViewModel()
        {
            Id = order.Id,
            Address = order.Address,
            Cart = ConvertToCartViewModel(order.Cart),
            CommentsToCourier = order.CommentsToCourier,
            StateOfOrder = order.StateOfOrder,
            TimeOfOrder = order.TimeOfOrder,
            User = order.User
        };

        public static UserViewModel ConvertToUserViewModel(User user) => new UserViewModel()
        {
            Id = user.Id,
            Addresses = user.Addresses,
            LastAddress = user.LastAddress,
            Favorites = user.Favorites.Select(ConvertToProductViewModel).ToList(),
            Login = user.Login,
            Password = user.Password,
            Role = user.Role
        };
    }
}
