using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
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


            var dto = res.Data;
            var vm = new DashboardViewModel
            {
                TotalProducts = dto.TotalProducts,
                TotalUsers = dto.TotalUsers,
                TotalOrders = dto.TotalOrders,
                TotalSales = dto.TotalSales,
            };

            for (int i = 0; i < dto.OrderDates.Count; i++)
            {
                vm.OrderDates.Add(dto.OrderDates[i]);
                vm.OrdersPerDay.Add(dto.OrdersPerDay[i]);
            }

            return View(vm);
        }
    }
}
