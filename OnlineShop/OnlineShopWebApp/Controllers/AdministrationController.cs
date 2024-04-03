using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IProductsRepository productsRepository;
        public AdministrationController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Products() 
        {
            var products = productsRepository.GetAll();
            return View(products);
        }
        public IActionResult Orders() => View();
        public IActionResult Users() => View();
        public IActionResult Roles() => View();
        public IActionResult DeleteProduct(int productId)
        {
            productsRepository.DeleteProduct(productId);
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int productId)
        {
            var product = productsRepository.TryGetById(productId);
            var productEditModel = ModelConverter.ConvertToProductViewModel(product);
            return View(productEditModel);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel product)
        {
            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if(!ModelState.IsValid)
                return View(product);

            productsRepository.EditProduct(product);
            return RedirectToAction("Products");
        }

        public IActionResult AddProduct() => View();

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel product)
        {
            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            productsRepository.AddProduct(product);
            return RedirectToAction("Products");
        }
    }
}
