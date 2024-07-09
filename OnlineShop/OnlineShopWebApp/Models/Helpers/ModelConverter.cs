using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Carts;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Roles;
using OnlineShopWebApp.Models.Users;
using System.Runtime.CompilerServices;

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
                City = address.City,
                Street = address.Street,
                House = address.House,
                Flat = address.Flat
            };
        }


        public static OrderViewModel? ToOrderViewModel(this Order order, string role = null)
        {
            if (order == null)
                return null;

            return new OrderViewModel()
            {
                Id = order.Id,
                Address = ToAddressViewModel(order.Address),
                Cart = new CartViewModel() { Items = order.CartItems.Select(ToCartItemViewModel).ToList(), UserId = order.UserId },
                CommentsToCourier = order.CommentsToCourier,
                StateOfOrder = order.StateOfOrder,
                TimeOfOrder = order.TimeOfOrder,
                User = order.User.ToUserViewModel(role)
            };
        }

        public static UserViewModel? ToUserViewModel(this User user, string? role = null)
        {
            if (user == null)
                return null;

            return new UserViewModel()
            {
                Id = user.Id,
                Addresses = user.Addresses?.Select(ToAddressViewModel).ToList(),
                Favorites = user.UserProductFavorites.Select(fav => fav.Product.ToProductViewModel()).ToList(),
                Login = user.UserName,
                Password = user.PasswordHash,
                Role = role
            };

        }
        public static RoleViewModel ToRoleViewModel(this IdentityRole role)
        {
            if (role == null)
                return null;

            return new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name!,
            };
        }
    }
}