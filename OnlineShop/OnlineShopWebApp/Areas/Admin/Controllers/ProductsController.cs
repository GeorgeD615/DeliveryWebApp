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

        public async Task<ActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            return View(products.Select(product => product.ToProductViewModel()).ToList());
        }

        public async Task<ActionResult> RemoveById(Guid productId)
        {
            await productsRepository.DeleteProductAsync(productId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(Guid productId)
        {
            var product = await productsRepository.TryGetByIdAsync(productId);

            if (product == null)
                return RedirectToAction(nameof(Index));

            var productEditModel = product.ToEditProductViewModel();
            return View(productEditModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(EditProductViewModel product)
        {
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            var uploadedImagesPaths = imagesProvider.SafeFiles(product.UploadedFiles, ImageFolder.products);

            await productsRepository.EditAsync(product.ToProduct(uploadedImagesPaths));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<ActionResult> AddAsync(AddProductViewModel product)
        {
            product.Name = product.Name.Trim();
            product.Description = product.Description.Trim();

            if (product.Name == product.Description)
                ModelState.AddModelError("", "Название и описание блюда не должны совпадать");

            if (!ModelState.IsValid)
                return View(product);

            var imagesPaths = imagesProvider.SafeFiles(product.UploadedFiles, ImageFolder.products);

            await productsRepository.AddAsync(product.ToProduct(imagesPaths));
            return RedirectToAction(nameof(Index));
        }
    }
}
