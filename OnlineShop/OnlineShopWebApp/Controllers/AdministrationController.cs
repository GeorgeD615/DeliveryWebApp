using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.Models.Products;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IRolesRepository rolesRepository;
        public AdministrationController(IProductsRepository productsRepository, IOrdersRepository ordersRepository, IRolesRepository rolesRepository)
        {
            this.productsRepository = productsRepository;
            this.ordersRepository = ordersRepository;
            this.rolesRepository = rolesRepository;
        }

        public IActionResult Products()
        {
            var products = productsRepository.GetAll();
            return View(products);
        }
        public IActionResult Orders()
        {
            var orders = ordersRepository.GetAll();
            return View(orders);
        } 
        public IActionResult Users() => View();
        public IActionResult Roles()
        {
            var roleFormModel = new RoleCreateFormViewModel();
            roleFormModel.roles = rolesRepository.GetAll();
            return View(roleFormModel);
        }

        [HttpPost]
        public IActionResult Roles(RoleCreateFormViewModel roleCreateFormViewModel)
        {
            var roleName = roleCreateFormViewModel.Name;
            if (rolesRepository.IsExisting(roleName))
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View(new RoleCreateFormViewModel() { roles = rolesRepository.GetAll() });

            rolesRepository.AddRole(new Role(roleName));

            return RedirectToAction("Roles");
        }

        public IActionResult RemoveRole(int roleId)
        {
            rolesRepository.RemoveRole(roleId);
            return RedirectToAction("Roles");
        }

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

        public IActionResult ShowOrderInfo(int orderId)
        {
            var order = ordersRepository.TryGetOrder(orderId);
            return View(order);
        }

        public IActionResult EditOrderStatus(int orderId, StateOfOrder status)
        {
            ordersRepository.EditStatus(orderId, status);
            return RedirectToAction("ShowOrderInfo", new { orderId });
        }
    }
}
