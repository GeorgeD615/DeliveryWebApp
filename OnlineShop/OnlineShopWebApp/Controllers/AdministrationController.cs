using Microsoft.AspNetCore.Mvc;
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
            var productEditModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = product.ImagePath,
            };
            return View(productEditModel);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel productEditModel)
        {
            productsRepository.EditProduct(productEditModel);
            return RedirectToAction("Products");
        }

        public IActionResult AddProduct() => View();

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel productCreateModel)
        {
            productsRepository.AddProduct(productCreateModel);
            return RedirectToAction("Products");
        }
    }
}
