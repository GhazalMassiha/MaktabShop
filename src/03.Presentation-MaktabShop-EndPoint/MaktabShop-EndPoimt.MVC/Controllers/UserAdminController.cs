using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
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

            var vm = res.Data
               .Select(u => new UserAdminViewModel
               {
                   Id = u.Id,
                   Username = u.Username,
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   Wallet = u.Wallet

               }).ToList();

            return View(res.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            logger.LogInformation("ادمین: مشاهده جزئیات کاربر {UserId}", id); 
            var res = await userAppService.GetById(id, CancellationToken.None);

            if (!res.IsSuccess || res.Data == null)
                return NotFound();

            var user = res.Data;
            var vm = new UserAdminViewModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Wallet = user.Wallet

            };

            return View(res.Data);
        }
    }
}
