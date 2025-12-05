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
        IProductAppService productAppService) : Controller
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
            var cart = GetCartFromSession();
            if (cart.Items == null || !cart.Items.Any())
            {
                return RedirectToAction("Index", "Order");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var walletRes = await userAppService.GetUserWalletBalance(userId.Value, CancellationToken.None);
            if (!walletRes.IsSuccess)
            {
                return View("Error");
            }

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
            var cart = GetCartFromSession();
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            decimal total = cart.Total;
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
                return View("Error");
            }

            ClearCartSession();
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}