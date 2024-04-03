using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Orders
{
    public enum StateOfOrder
    {
        [Display(Name = "Обрабатывается")]
        InProcessing,
        [Display(Name = "Готовится")]
        Cooking,
        [Display(Name = "Доставляется")]
        Delivering,
        [Display(Name = "Доставлен")]
        Delevered,
        [Display(Name = "Отменён")]
        Cancelled
    }
}
