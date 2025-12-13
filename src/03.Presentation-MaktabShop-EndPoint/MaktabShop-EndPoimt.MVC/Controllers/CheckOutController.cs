using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class CheckoutController(IUserAppService userAppService, IOrderAppService orderAppService,
        ILogger<CheckoutController> logger) : Controller
    {

        private const string SessionCartKey = "CartSession";

        private OrderViewModel GetCartFromSession()
        {
            string? cartJson = HttpContext.Session.GetString(SessionCartKey);
            if (string.IsNullOrEmpty(cartJson))
                return new OrderViewModel();

            return JsonSerializer.Deserialize<OrderViewModel>(cartJson)!;
        }

        private void ClearCartSession()
        {
            HttpContext.Session.Remove(SessionCartKey);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("نمایش صفحه تسویه حساب");

            var cart = GetCartFromSession();
            if (cart.Items == null || !cart.Items.Any())
            {
                logger.LogWarning("سبد خرید خالی است و به صفحه سفارش هدایت شد");
                return RedirectToAction("Index", "Order");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                logger.LogWarning("کاربر لاگین نکرده و به صفحه ورود هدایت شد");
                return RedirectToAction("Login", "Account");
            }

            var walletRes = await userAppService.GetUserWalletBalance(userId.Value, CancellationToken.None);
            if (!walletRes.IsSuccess)
            {
                logger.LogError("خطا در دریافت موجودی کیف پول برای کاربر {UserId}", userId);
                return View("Error");
            }

            logger.LogInformation("کیف پول کاربر {UserId} بازیابی شد: {Balance}", userId, walletRes.Data);

            var vm = new CheckoutViewModel
            {
                Order = cart,
                WalletBalance = walletRes.Data
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            logger.LogInformation("کاربر {UserId} درخواست نهایی کردن سفارش را ارسال کرد", userId);

            if (userId == null)
            {
                logger.LogWarning("کاربر ناشناس در تلاش برای نهایی کردن سفارش");
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartFromSession();
            decimal total = cart.Total;
            logger.LogInformation("جمع کل سفارش کاربر {UserId} معادل {Total}", userId, total);

            var orderDto = new OrderCreateDto
            {
                UserId = userId.Value,
                TotalPrice = total,
                OrederItems = cart.Items.Select(i => new OrderItemCreateDto
                {
                    ProductId = i.ProductId,
                    Count = i.Stock,
                    UnitPrice = i.UnitPrice

                }).ToList()
            };

            var res = await orderAppService.Create(orderDto, CancellationToken.None);
            if (!res.IsSuccess)
            {
                logger.LogError("خطا در ثبت سفارش برای کاربر {UserId}: {Message}", userId, res.Message);
                return View("Error");
            }

            logger.LogInformation("سفارش کاربر {UserId} با موفقیت ثبت شد", userId);

            ClearCartSession();
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            logger.LogInformation("صفحه موفقیت ثبت سفارش نمایش داده شد");
            return View();
        }
    }
}