using System.ComponentModel.DataAnnotations;

namespace MaktabShop_EndPoimt.MVC.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام دسته بندی الزامی است.")]
        public string Name { get; set; }
    }
}
