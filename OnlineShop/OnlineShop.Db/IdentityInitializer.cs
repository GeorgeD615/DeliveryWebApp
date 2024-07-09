using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminLogin = "george";
            var password = "Ab123456_";
            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
            {
                var adminRole = new IdentityRole() { Name = Constants.AdminRoleName };
                roleManager.CreateAsync(adminRole).Wait();
            }
            if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
            {
                var userRole = new IdentityRole() { Name = Constants.UserRoleName };
                roleManager.CreateAsync(userRole).Wait();
            }
            if (userManager.FindByNameAsync(adminLogin).Result == null)
            {
                var admin = new User { UserName = adminLogin };
                var result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();
                }
            }
        }
    }
}
