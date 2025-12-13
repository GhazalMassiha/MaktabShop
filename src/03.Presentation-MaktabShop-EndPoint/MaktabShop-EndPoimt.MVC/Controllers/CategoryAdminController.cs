using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryAdminController(ICategoryAppService categoryAppService, ILogger<CategoryAdminController> logger) : Controller
    {

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("ادمین: مشاهده لیست دسته‌بندی‌ها");
            var res = await categoryAppService.GetAll(CancellationToken.None);

            if (!res.IsSuccess)
            {
                logger.LogError("خطا در دریافت لیست دسته بندی ها برای پنل مدیریت");
                return View("Error");
            }

            return View(res.Data);
        }

        public IActionResult Create()
        {
            logger.LogInformation("ادمین: باز کردن فرم ایجاد دسته‌بندی");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ادمین: اطلاعات ورودی فرم ایجاد دسته‌بندی نامعتبر است");
                return View(dto);
            }

            logger.LogInformation("ادمین: در حال ایجاد دسته‌بندی {CategoryName}", dto.Name);
            await categoryAppService.AddCategory(dto, CancellationToken.None);

            logger.LogInformation("ادمین: دسته‌بندی {CategoryName} با موفقیت ایجاد شد", dto.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            logger.LogInformation("ادمین: باز کردن فرم ویرایش دسته‌بندی {CategoryId}", id);
            var res = await categoryAppService.GetById(id, CancellationToken.None);

            return View(res.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ادمین: اطلاعات ورودی فرم ویرایش دسته‌بندی نامعتبر است"); 
                return View(dto);
            }

            logger.LogInformation("ادمین: در حال بروزرسانی دسته‌بندی {CategoryId}", id);
            await categoryAppService.UpdateCategory(id, dto, CancellationToken.None);

            logger.LogInformation("ادمین: دسته‌بندی {CategoryId} با موفقیت بروزرسانی شد", id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("ادمین: درخواست حذف دسته‌بندی {CategoryId}", id);
            await categoryAppService.DeleteCategory(id, CancellationToken.None);

            logger.LogInformation("ادمین: دسته‌بندی {CategoryId} حذف شد", id);
            return RedirectToAction(nameof(Index));
        }
    }
}
