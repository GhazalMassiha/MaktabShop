using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController(IOrderAppService orderAppService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var res = await orderAppService.GetDashboardStats(CancellationToken.None);

            if (!res.IsSuccess) 
                return View("Error");

            return View(res.Data);
        }
    }
}
