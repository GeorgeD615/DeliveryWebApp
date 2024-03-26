using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult Orders() => View();
        public IActionResult Users() => View();
        public IActionResult Roles() => View();
        public IActionResult Products() => View();
    }
}
