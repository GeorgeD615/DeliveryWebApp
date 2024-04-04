﻿using OnlineShopWebApp.Models.Orders;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OnlineShopWebApp.Models
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }

        public static List<StateOfOrder> GetStatetsOfOrderList() => Enum.GetValues(typeof(StateOfOrder)).Cast<StateOfOrder>().ToList();
    }
}
