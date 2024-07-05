using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShop.Db
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminLogin = "george";
            var password = "Ab123456_";
            if (roleManager.FindByNameAsync(Constants.adminRoleName).Result == null)
            {
                var adminRole = new IdentityRole() { Name = Constants.adminRoleName };
                roleManager.CreateAsync(adminRole).Wait();
            }
            if (roleManager.FindByNameAsync(Constants.userRoleName).Result == null)
            {
                var userRole = new IdentityRole() { Name = Constants.userRoleName };
                roleManager.CreateAsync(userRole).Wait();
            }
            if (userManager.FindByNameAsync(adminLogin).Result == null)
            {
                var admin = new User { UserName = adminLogin };
                var result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, Constants.adminRoleName).Wait();
                }
            }
        }
    }
}
