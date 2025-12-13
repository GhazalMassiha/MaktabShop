using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;

public class CategoryController(IProductAppService productAppService, ILogger<CategoryController> logger) : Controller
{

    public async Task<IActionResult> List(int id)
    {
        logger.LogInformation("نمایش محصولات برای دسته‌بندی {CategoryId}", id);

        var prodRes = await productAppService.GetProductByCategoryId(id, CancellationToken.None);
        if (!prodRes.IsSuccess)
        {
            logger.LogWarning("خطا در دریافت محصولات برای دسته‌بندی {CategoryId}", id);
            return View("Error");
        }

        logger.LogInformation("محصولات دسته‌بندی {CategoryId} با موفقیت دریافت شد", id);

        var vm = prodRes.Data.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Stock = p.Stock,
            CategoryId = p.CategoryId,
            CategoryName = p.CategoryName,
            Description = p.Description

        }).ToList();

        return View(vm);
    }
}