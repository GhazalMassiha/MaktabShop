using System.ComponentModel.DataAnnotations;

namespace MaktabShop_EndPoimt.MVC.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام کالا الزامی است.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "توضیحات کالا نمیتواند خالی باشد.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "قیمت کالا الزامی است.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "عکس کالا الزامی است.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "موجودی کالا الزامی است.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "شناسه دسته بندی کالا الزامی است.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "نام دسته بندی کالا الزامی است.")]
        public string CategoryName { get; set; }
    }
}
