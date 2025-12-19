using System.ComponentModel.DataAnnotations;

namespace MaktabShop_EndPoimt.MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "نام الزامی است.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "شماره همراه الزامی است.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "آدرس الزامی است.")]
        public string Address { get; set; }
    }
}
