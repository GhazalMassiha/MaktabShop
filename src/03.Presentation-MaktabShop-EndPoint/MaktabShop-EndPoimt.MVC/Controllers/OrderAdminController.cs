using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
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

            return View(res.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            logger.LogInformation("ادمین: مشاهده جزئیات سفارش {OrderId}", id); 
            var res = await orderAppService.GetOrderDetails(id, CancellationToken.None);

            return View(res.Data);
        }
    }
}
