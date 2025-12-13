using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

public class ProductController(IProductAppService productAppService, ILogger<ProductController> logger) : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        logger.LogInformation("درخواست جزئیات محصول {ProductId}", id);

        var prodRes = await productAppService.GetById(id, CancellationToken.None);

        if (!prodRes.IsSuccess || prodRes.Data == null)
        {
            logger.LogWarning("محصول {ProductId} یافت نشد", id);
            return NotFound();
        }

        logger.LogInformation("جزئیات محصول {ProductId} با موفقیت دریافت شد", id);

        var p = prodRes.Data;
        var vm = new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Stock = p.Stock,
            CategoryId = p.CategoryId,
            CategoryName = p.CategoryName,
            Description = p.Description
        };

        return View(vm);
    }
}