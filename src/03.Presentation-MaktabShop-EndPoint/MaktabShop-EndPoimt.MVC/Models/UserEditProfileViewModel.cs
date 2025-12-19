using System.ComponentModel.DataAnnotations;

namespace MaktabShop_EndPoimt.MVC.Models
{
    public class UserEditProfileViewModel
    {
        [Required(ErrorMessage = "نام الزامی است.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Display(Name = "کیف پول")]
        public decimal Wallet { get; set; }
    }
}
