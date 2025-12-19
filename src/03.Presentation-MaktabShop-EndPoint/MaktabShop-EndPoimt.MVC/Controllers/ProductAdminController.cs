using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductAdminController(IProductAppService productAppService, IWebHostEnvironment env,
        ILogger<ProductAdminController> logger) : Controller
    {

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("ادمین: مشاهده لیست محصولات");
            var res = await productAppService.GetAll(CancellationToken.None);

            if (!res.IsSuccess)
            {
                logger.LogError("خطا در دریافت لیست محصولات برای پنل مدیریت"); 
                return View("Error");
            }

            var vm = res.Data.Select(p => new ProductAdminViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.CategoryName

            }).ToList();

            return View(vm);
        }

        public IActionResult Create()
        {
            logger.LogInformation("ادمین: باز کردن فرم ایجاد محصول");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {

            if (!ModelState.IsValid)
            {
                logger.LogWarning("ادمین: اطلاعات ورودی فرم ایجاد محصول نامعتبر است"); 
                return View(dto);
            }

            logger.LogInformation("ادمین: در حال ایجاد محصول با نام {ProductName}", dto.Name);
            var res = await productAppService.AddProduct(dto, CancellationToken.None);

            if (!res.IsSuccess)
            {
                logger.LogError("خطا در ایجاد محصول با نام {ProductName}", dto.Name); 
                return View(dto);
            }

            logger.LogInformation("ادمین: محصول {ProductName} با موفقیت ایجاد شد", dto.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            logger.LogInformation("ادمین: باز کردن فرم ویرایش محصول با شناسه {ProductId}", id);
            var res = await productAppService.GetById(id, CancellationToken.None);

            if (!res.IsSuccess || res.Data == null)
            {
                logger.LogWarning("ادمین: محصول با شناسه {ProductId} پیدا نشد", id); 
                return NotFound();
            }

            var p = res.Data;

            var vm = new ProductCreateDto
            {
                Name = p.Name,
                CategoryId = p.CategoryId,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                Description = p.Description
            };


            return View(res.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ادمین: اطلاعات ورودی فرم ویرایش محصول نامعتبر است");
                return View(dto);
            }

            logger.LogInformation("ادمین: در حال بروزرسانی محصول {ProductId}", id);
            var res = await productAppService.UpdateProduct(id, dto, CancellationToken.None);

            if (!res.IsSuccess)
            {
                logger.LogError("خطا در بروزرسانی محصول {ProductId}", id);
                return View(dto);
            }

            logger.LogInformation("ادمین: محصول {ProductId} با موفقیت بروزرسانی شد", id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("ادمین: درخواست حذف محصول {ProductId}", id);
            await productAppService.DeleteProduct(id, CancellationToken.None);

            logger.LogInformation("ادمین: محصول {ProductId} حذف شد", id);
            return RedirectToAction(nameof(Index));
        }
    }
}
