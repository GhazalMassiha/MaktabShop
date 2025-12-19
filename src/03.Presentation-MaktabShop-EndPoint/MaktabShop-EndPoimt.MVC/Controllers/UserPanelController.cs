using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class UserPanelController(UserManager<AppUser> userManager, IOrderAppService orderAppService, ILogger<UserAdminController> logger) 
        : Controller
    {

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            logger.LogInformation("نمایش صفحه پروفایل برای کاربر {Username}", user?.UserName);

            if (user == null)
            {
                logger.LogWarning("کاربر نامشخص درخواست پروفایل داده است");
                return RedirectToAction("Login", "Account");
            }

            var vm = new UserProfileViewModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Wallet = user.Wallet
            };

            logger.LogInformation("پروفایل کاربر {Username} آماده نمایش شد", user.UserName);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            logger.LogInformation("درخواست نمایش فرم ویرایش پروفایل برای {Username}", user?.UserName);

            if (user == null)
            {
                logger.LogWarning("کاربر نامشخص درخواست پروفایل داده است");
                return RedirectToAction("Login", "Account");
            }

            var vm = new UserEditProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditProfileViewModel vm)
        {
            logger.LogInformation("درخواست ثبت تغییرات پروفایل برای {Username}", User.Identity?.Name);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("داده‌های ورودی برای ویرایش نامعتبر است برای {Username}", User.Identity?.Name);
                return View(vm);
            }
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                logger.LogWarning("کاربر {Username} برای ویرایش یافت نشد", User.Identity?.Name);
                return RedirectToAction("Login", "Account");
            }

            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.PhoneNumber = vm.PhoneNumber;
            user.Address = vm.Address;
            user.Wallet = vm.Wallet;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    logger.LogWarning("خطا هنگام بروزرسانی {Username}: {Error}", user.UserName, error.Description);
                }
                return View(vm);
            }

            logger.LogInformation("پروفایل {Username} با موفقیت بروزرسانی شد", user.UserName);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            logger.LogInformation("درخواست نمایش فرم تغییر رمز برای {Username}", User.Identity?.Name);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            logger.LogInformation("درخواست ثبت تغییر رمز برای {Username}", User.Identity?.Name);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("داده‌های واردشده برای تغییر رمز نامعتبر است برای {Username}", User.Identity?.Name);
                return View(vm);
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                logger.LogWarning("کاربر {Username} یافت نشد برای تغییر رمز", User.Identity?.Name);
                return RedirectToAction("Login", "Account");
            }

            var result = await userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    logger.LogWarning("خطا در تغییر رمز برای {Username}: {Error}", user.UserName, error.Description);
                }
                return View(vm);
            }

            logger.LogInformation("رمز عبور برای {Username} با موفقیت تغییر یافت", user.UserName);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Orders()
        {
            var user = await userManager.GetUserAsync(User);
            logger.LogInformation("درخواست مشاهده سفارش‌های کاربر {Username}", user?.UserName);

            if (user == null)
            {
                logger.LogWarning("کاربر برای مشاهده سفارش‌ها لاگین نیست");
                return RedirectToAction("Login", "Account");
            }

            var res = await orderAppService.GetAllOrders(CancellationToken.None);


            if (!res.IsSuccess)
            {
                logger.LogError("خطا در بازیابی لیست سفارش‌ها برای {Username}", user.UserName);
                return View("Error");
            }


            var userOrders = res.Data
                .Where(o => o.UserId == user.Id)
                .ToList();

            logger.LogInformation("تعداد {Count} سفارش برای {Username} بازیابی شد", userOrders.Count, user.UserName);

            return View(userOrders);
        }
    }
}
