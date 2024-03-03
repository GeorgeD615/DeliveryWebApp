﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            ProductsInfo.Products.TryGetValue(id, out Product result);

            if (result != null)
                return $"{id}\n{result.Name}\n{result.Cost}\n{result.Description}";

            return "Товар не найден:(";
        }
    }
}
