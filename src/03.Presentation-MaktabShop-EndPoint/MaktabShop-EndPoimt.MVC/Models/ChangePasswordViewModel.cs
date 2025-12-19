using System.ComponentModel.DataAnnotations;

namespace MaktabShop_EndPoimt.MVC.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "رمز عبور قبلی الزامی است.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "رمز عبور جدید الزامی است.")]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "رمز جدید مطابقت ندارد.")]
        public string ConfirmNewPassword { get; set; }
    }
}
