using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Enums;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class AccountController(IUserAppService userAppService, ILogger<AccountController> logger) : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {
            logger.LogInformation("صفحه ثبت‌نام نمایش داده شد");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            logger.LogInformation("کاربر در تلاش برای ثبت‌نام با نام کاربری {Username}", vm.Username);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("ثبت‌نام ناموفق: داده‌های ورودی نامعتبر است برای {Username}", vm.Username);
                return View(vm);
            }

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
                logger.LogWarning("ثبت‌نام {Username} با خطا روبرو شد: {Message}", vm.Username, res.Message);

                ModelState.AddModelError("", res.Message);
                return View(vm);
            }

            logger.LogInformation("کاربر {Username} با موفقیت ثبت‌نام کرد", vm.Username);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            logger.LogInformation("صفحه ورود نمایش داده شد");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            logger.LogInformation("کاربر در تلاش برای ورود با نام کاربری {Username}", vm.Username);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("ورود نامعتبر: داده‌های ورودی نامعتبر برای {Username}", vm.Username);
                return View(vm);
            }

            var res = await userAppService.Login(vm.Username, vm.Password, CancellationToken.None);

            if (!res.IsSuccess || res.Data == null)
            {
                logger.LogWarning("ورود ناموفق برای {Username}: {Message}", vm.Username, res.Message);
                ModelState.AddModelError("", res.Message);
                return View(vm);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, res.Data.Username),
                new Claim(ClaimTypes.NameIdentifier, res.Data.Id.ToString()),
                new Claim(ClaimTypes.Role, res.Data.Role.ToString()) 
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

        
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true }
            );

            logger.LogInformation("کاربر {Username} با موفقیت وارد شد با نقش {Role}", vm.Username, res.Data.Role);

   
            if (res.Data.Role == RoleEnum.Admin)
            {
                logger.LogInformation("ادمین وارد شد، هدایت به پنل مدیریت");
                return RedirectToAction("Index", "AdminDashboard");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var username = HttpContext.Session.GetString("Username");
            logger.LogInformation("کاربر {Username} از سیستم خارج شد", username);

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}