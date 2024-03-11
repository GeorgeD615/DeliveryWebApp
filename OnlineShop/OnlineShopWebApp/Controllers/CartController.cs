using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index() => View(Cart.GetAll());

        public IActionResult AddProductToCart(int id)
        {
            Cart.AddProduct(ProductsRepository.TryGetById(id));
            return View("Index", Cart.GetAll());
        }
    }
}
