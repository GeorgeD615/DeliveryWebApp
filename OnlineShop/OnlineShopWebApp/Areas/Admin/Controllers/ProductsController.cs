using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(products);
        }

        public IActionResult RemoveById(Guid productId)
        {
            productsRepository.DeleteProduct(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);

            if (product == null)
                return RedirectToAction("Index");

            var productEditModel = ModelConverter.ConvertToProductViewModel(product);
            return View(productEditModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            productsRepository.Edit(product);
            return RedirectToAction("Index");
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            productsRepository.Add(product);
            return RedirectToAction("Index");
        }
    }
}
