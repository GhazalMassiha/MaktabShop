using System.ComponentModel.DataAnnotations;


namespace MaktabShop_EndPoimt.MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
