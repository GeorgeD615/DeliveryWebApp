using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enums
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
