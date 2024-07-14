using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Products;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ImagesProvider imagesProvider;

        public ProductsController(IProductsRepository productsRepository, ImagesProvider imagesProvider)
        {
            this.productsRepository = productsRepository;
            this.imagesProvider = imagesProvider;
        }

        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(products.Select(product => product.ToProductViewModel()).ToList());
        }

        public IActionResult RemoveById(Guid productId)
        {
            productsRepository.DeleteProduct(productId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);

            if (product == null)
                return RedirectToAction(nameof(Index));

            var productEditModel = product.ToEditProductViewModel();
            return View(productEditModel);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel product)
        {
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            var uploadedImagesPaths = imagesProvider.SafeFiles(product.UploadedFiles, ImageFolder.products);

            productsRepository.Edit(product.ToProduct(uploadedImagesPaths));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddProductViewModel product)
        {
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            var imagesPaths = imagesProvider.SafeFiles(product.UploadedFiles, ImageFolder.products);


            productsRepository.Add(product.ToProduct(imagesPaths));
            return RedirectToAction(nameof(Index));
        }
    }
}
