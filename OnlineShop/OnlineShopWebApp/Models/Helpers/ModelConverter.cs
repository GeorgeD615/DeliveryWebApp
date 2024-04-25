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
        public static ProductViewModel? ToProductViewModel(this Product product)
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

        public static Product? ToProduct(this ProductViewModel productViewModel)
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

        public static CartItemViewModel? ToCartItemViewModel(this CartItem cartItem)
        {
            if (cartItem == null)
                return null;

            return new CartItemViewModel(
                cartItem.Id,
                ToProductViewModel(cartItem.Product),
                cartItem.Amount);
        }

        public static CartViewModel? ToCartViewModel(this Cart cart)
        {
            if (cart == null)
                return null;

            return new CartViewModel(
                cart.Id,
                cart.UserId,
                cart.Items.Select(ToCartItemViewModel).ToList());
        }

        public static AddressViewModel ToAddressViewModel(this Address address)
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

        public static Address ToAddress(this AddressViewModel address)
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


        public static OrderViewModel? ToOrderViewModel(this Order order)
        {
            if (order == null)
                return null;

            return new OrderViewModel()
            {
                Id = order.Id,
                Address = ToAddressViewModel(order.Address),
                Cart = new CartViewModel() { Items = order.CartItems.Select(ToCartItemViewModel).ToList(), UserId = order.Id },
                CommentsToCourier = order.CommentsToCourier,
                StateOfOrder = order.StateOfOrder,
                TimeOfOrder = order.TimeOfOrder,
                User = ToUserViewModel(order.User)
            };
        }

        public static UserViewModel? ToUserViewModel(this User user)
        {
            if (user == null)
                return null;

            return new UserViewModel()
            {
                Id = user.Id,
                Addresses = user.Addresses?.Select(ToAddressViewModel).ToList(),
                Favorites = user.Favorites?.Select(ToProductViewModel).ToList(),
                Login = user.Login,
                Password = user.Password,
                Role = ToRoleViewModel(user.Role)
            };

        }
        public static RoleViewModel ToRoleViewModel(this Role role)
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
