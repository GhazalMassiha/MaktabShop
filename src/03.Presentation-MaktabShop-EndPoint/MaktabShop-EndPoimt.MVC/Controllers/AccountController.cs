using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.Enums;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class AccountController(IUserAppService userAppService, ILogger<AccountController> logger,
                SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
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

            var user = new AppUser
            {
                UserName = vm.Username,
                PasswordHash = vm.Password,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.Phone,
                Address = vm.Address
            };

            var res = await userManager.CreateAsync(user, vm.Password);

            if (!res.Succeeded)
            {
                logger.LogWarning("ثبت‌نام {Username} با خطا روبرو شد", vm.Username);

                foreach (var error in res.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(vm);
            }

            logger.LogInformation("کاربر {Username} با موفقیت ثبت‌نام کرد", vm.Username);
            await signInManager.SignInAsync(user, isPersistent: false);
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

            var res = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, isPersistent: true, lockoutOnFailure: false);

            if (!res.Succeeded)
            {
                logger.LogWarning("ورود ناموفق برای {Username}", vm.Username);
                ModelState.AddModelError("", "نام کاربری یا رمز اشتباه است");
                return View(vm);
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, res.Data.Username),
            //    new Claim(ClaimTypes.NameIdentifier, res.Data.Id.ToString()),
            //    new Claim(ClaimTypes.Role, res.Data.Role.ToString())
            //};

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);


            //await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    principal,
            //    new AuthenticationProperties { IsPersistent = true }
            //);


            //if (res.Data.Role == RoleEnum.Admin)
            //{
            //    logger.LogInformation("ادمین وارد شد، هدایت به پنل مدیریت");
            //    return RedirectToAction("Index", "AdminDashboard");
            //}

            var user = await userManager.FindByNameAsync(vm.Username);

            if (user != null && await userManager.IsInRoleAsync(user, RoleEnum.Admin.ToString()))
            {
                logger.LogInformation("ادمین وارد شد، هدایت به پنل مدیریت");
                return RedirectToAction("Index", "AdminDashboard");
            }

            logger.LogInformation("کاربر {Username} با موفقیت وارد شد", vm.Username);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //var username = HttpContext.Session.GetString("Username");
            //logger.LogInformation("کاربر {Username} از سیستم خارج شد", username);

            //HttpContext.Session.Clear();

            await signInManager.SignOutAsync();

            logger.LogInformation("کاربر {Username} از سیستم خارج شد");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}