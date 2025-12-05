using System.ComponentModel.DataAnnotations;

namespace MaktabShop_EndPoimt.MVC.Models
{
    public class OrderItemViewModel
    {
        [Required(ErrorMessage = "شناسه کالا الزامی است.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "نام کالا الزامی است.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "عکس کالا الزامی است.")]
        public string ProductImageUrl { get; set; }

        [Required(ErrorMessage = "قیمت واحد کالا الزامی است.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "موجودی کالا الزامی است.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "قیمت کل کالا الزامی است.")]
        public decimal TotalPrice => UnitPrice * Stock;
    }
}
