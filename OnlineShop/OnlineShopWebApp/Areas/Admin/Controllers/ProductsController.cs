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
        private readonly IWebHostEnvironment appEnvironment;

        public ProductsController(IProductsRepository productsRepository, IWebHostEnvironment appEnvironment)
        {
            this.productsRepository = productsRepository;
            this.appEnvironment = appEnvironment;
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

            var productEditModel = product.ToProductViewModel();
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

            if (product.UploadedFile != null)
            {
                var productImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                if (!Directory.Exists(productImagesPath))
                    Directory.CreateDirectory(productImagesPath);

                var fileName = Guid.NewGuid() + "." + product.UploadedFile.FileName.Split('.').Last();
                using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    product.UploadedFile.CopyTo(fileStream);

                product.ImagePath = "/images/products/" + fileName;
            }


            productsRepository.Edit(product.ToProduct());
            return RedirectToAction(nameof(Index));
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

            if(product.UploadedFile != null)
            {
                var productImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                if(!Directory.Exists(productImagesPath))
                    Directory.CreateDirectory(productImagesPath); 

                var fileName = Guid.NewGuid() + "." + product.UploadedFile.FileName.Split('.').Last();
                using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    product.UploadedFile.CopyTo(fileStream);

                product.ImagePath = "/images/products/" + fileName;
            }
            else
            {
                product.ImagePath = "/images/products/" + "default_product.jpg";
            }


            productsRepository.Add(product.ToProduct());
            return RedirectToAction(nameof(Index));
        }
    }
}
