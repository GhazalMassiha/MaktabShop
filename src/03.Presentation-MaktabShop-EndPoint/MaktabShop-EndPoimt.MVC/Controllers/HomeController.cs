using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController(IProductAppService productAppService, ICategoryAppService categoryAppService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var catRes = await categoryAppService.GetAll(CancellationToken.None);
        var prodRes = await productAppService.GetAll(CancellationToken.None);

        if (!catRes.IsSuccess || !prodRes.IsSuccess)
        {
            return View("Error");
        }

        var vm = new HomeIndexViewModel
        {
            Categories = catRes.Data
                .Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name })
                .ToList(),

            Products = prodRes.Data
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

                }).ToList()
        };

        return View(vm);
    }
}