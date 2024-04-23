using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Roles;
using OnlineShopWebApp.Models.Users;

namespace OnlineShopWebApp.Models.Helpers
{
    public static class ModelConverter
    {
        public static ProductViewModel? ConvertToProductViewModel(Product product)
        {
            if (product == null)
                return null;

            return new ProductViewModel(
                            product.Id,
                            product.Name,
                            product.Cost,
                            product.Description,
                            product.ImagePath);
        }

        public static Product? ConvertToProduct(ProductViewModel productViewModel)
        {
            if (productViewModel == null)
                return null;

            return new Product()
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Description = productViewModel.Description,
                ImagePath = productViewModel.ImagePath
            };
        }

        public static CartItemViewModel? ConvertToCartItemViewModel(CartItem cartItem)
        {
            if (cartItem == null)
                return null;

            return new CartItemViewModel(
                cartItem.Id,
                ConvertToProductViewModel(cartItem.Product),
                cartItem.Amount);
        }

        public static CartViewModel? ConvertToCartViewModel(Cart cart)
        {
            if (cart == null)
                return null;

            return new CartViewModel(
                cart.Id,
                cart.UserId,
                cart.Items.Select(ConvertToCartItemViewModel).ToList());
        }

        public static AddressViewModel ConvertToAddressViewModel(Address address)
        {
            if (address == null)
                return null;

            return new AddressViewModel()
            {
                Id = address.Id,
                UserId = address.UserId,
                City = address.City,
                Street = address.Street,
                House = address.House,
                Flat = address.Flat
            };
        }

        public static Address ConvertToAddress(AddressViewModel address)
        {
            if (address == null)
                return null;

            return new Address()
            {
                Id = address.Id,
                UserId = address.UserId,
                City = address.City,
                Street = address.Street,
                House = address.House,
                Flat = address.Flat
            };
        }


        public static OrderViewModel? ConvertToOrderViewModel(Order order)
        {
            if (order == null)
                return null;

            return new OrderViewModel()
            {
                Id = order.Id,
                Address = ConvertToAddressViewModel(order.Address),
                Cart = ConvertToCartViewModel(order.Cart),
                CommentsToCourier = order.CommentsToCourier,
                StateOfOrder = order.StateOfOrder,
                TimeOfOrder = order.TimeOfOrder,
                User = ConvertToUserViewModel(order.User)
            };
        }

        public static UserViewModel? ConvertToUserViewModel(User user)
        {
            if (user == null)
                return null;

            return new UserViewModel()
            {
                Id = user.Id,
                Addresses = user.Addresses?.Select(ConvertToAddressViewModel).ToList(),
                Favorites = user.Favorites?.Select(ConvertToProductViewModel).ToList(),
                Login = user.Login,
                Password = user.Password,
                Role = ConvertToRoleViewModel(user.Role)
            };

        }
        public static RoleViewModel ConvertToRoleViewModel(Role role)
        {
            if (role == null)
                return null;

            return new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}
