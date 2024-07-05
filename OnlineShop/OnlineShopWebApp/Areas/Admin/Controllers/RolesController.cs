﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db;
using OnlineShopWebApp.Models.Roles;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.adminRoleName)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> rolesManager;

        public RolesController(RoleManager<IdentityRole> rolesManager)
        {
            this.rolesManager = rolesManager;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(RoleViewModel role)
        {
            var roleName = role.Name.Trim().ToLower();

            if (rolesManager.RoleExistsAsync(roleName).Result)
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View(role);

            rolesManager.CreateAsync(new IdentityRole { Name = roleName, }).Wait();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveById(Guid roleId)
        {
            var role = rolesManager.FindByIdAsync(roleId.ToString()).Result;

            if (role != null)
                rolesManager.DeleteAsync(role).Wait();

            return RedirectToAction("Index");
        }
    }
}