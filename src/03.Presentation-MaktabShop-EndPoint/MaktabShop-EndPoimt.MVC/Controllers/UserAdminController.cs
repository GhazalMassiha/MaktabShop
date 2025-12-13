using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController(IUserAppService userAppService, ILogger<UserAdminController> logger) : Controller
    {

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("ادمین: مشاهده لیست کاربران"); 
            var res = await userAppService.GetAll(CancellationToken.None);

            return View(res.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            logger.LogInformation("ادمین: مشاهده جزئیات کاربر {UserId}", id); 
            var res = await userAppService.GetById(id, CancellationToken.None);

            return View(res.Data);
        }
    }
}
