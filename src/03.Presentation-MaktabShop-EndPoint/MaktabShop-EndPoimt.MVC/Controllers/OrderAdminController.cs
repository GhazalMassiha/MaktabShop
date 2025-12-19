using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderAdminController(IOrderAppService orderAppService, ILogger<OrderAdminController> logger) : Controller
    {

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("ادمین: مشاهده لیست سفارش‌ها"); 
            var res = await orderAppService.GetAllOrders(CancellationToken.None);

            var vm = res.Data
              .Select(o => new OrderAdminViewModel
              {
                  Id = o.Id,
                  UserId = o.UserId,
                  TotalPrice = o.TotalPrice

              }).ToList();

            return View(res.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            logger.LogInformation("ادمین: مشاهده جزئیات سفارش {OrderId}", id); 
            var res = await orderAppService.GetOrderDetails(id, CancellationToken.None);

            if (!res.IsSuccess || res.Data == null)
                return NotFound();

            return View(res.Data);
        }
    }
}
