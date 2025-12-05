using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class AccountController(IUserAppService userAppService) : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new Core_MaktabShop.Domain.Core.UserAgg.DTOs.UserCreateDto
            {
                Username = vm.Username,
                PasswordHash = vm.Password, 
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Phone = vm.Phone,
                Address = vm.Address
            };

            var res = await userAppService.Register(dto, CancellationToken.None);
            if (!res.IsSuccess)
            {
                ModelState.AddModelError("", res.Message ?? "خطا در ثبت نام");
                return View(vm);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var res = await userAppService.Login(vm.Username, vm.Password, CancellationToken.None);
            if (!res.IsSuccess || res.Data == null)
            {
                ModelState.AddModelError("", res.Message ?? "نام کاربری یا رمز عبور اشتباه است");
                return View(vm);
            }

            HttpContext.Session.SetInt32("UserId", res.Data.Id);
            HttpContext.Session.SetString("Username", res.Data.Username);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}