using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;

public class CategoryController(IProductAppService productAppService) : Controller
{

    public async Task<IActionResult> List(int id)
    {
        var prodRes = await productAppService.GetProductByCategoryId(id, CancellationToken.None);

        if (!prodRes.IsSuccess)
        {
            return View("Error");
        }

        var vm = prodRes.Data
                 .Select(p => new ProductViewModel
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