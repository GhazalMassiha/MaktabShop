using Microsoft.AspNetCore.Mvc;
using MaktabShop_EndPoimt.MVC.Models;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using System.Threading;

public class ProductController(IProductAppService productAppService) : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        var prodRes = await productAppService.GetById(id, CancellationToken.None);

        if (!prodRes.IsSuccess || prodRes.Data == null)
        {
            return NotFound();
        }

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