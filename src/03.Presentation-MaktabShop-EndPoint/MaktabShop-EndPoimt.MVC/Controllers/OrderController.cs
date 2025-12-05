using Microsoft.AspNetCore.Mvc;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using System.Text.Json;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class CartController(IProductAppService productAppService) : Controller
    {
        private const string SessionCartKey = "CartSession";


        private OrderViewModel GetCartFromSession()
        {
            var session = HttpContext.Session;
            string? cartJson = session.GetString(SessionCartKey);
            if (string.IsNullOrEmpty(cartJson))
                return new OrderViewModel();

            return JsonSerializer.Deserialize<OrderViewModel>(cartJson)!;
        }

        private void SaveCartSession(OrderViewModel cart)
        {
            var session = HttpContext.Session;
            session.SetString(SessionCartKey, JsonSerializer.Serialize(cart));
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int stock = 1)
        {
            var prodRes = await productAppService.GetById(productId, CancellationToken.None);
            if (!prodRes.IsSuccess || prodRes.Data == null)
                return NotFound();

            var p = prodRes.Data;

            var cart = GetCartFromSession();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == p.Id);
            if (existing != null)
            {
                existing.Stock += stock;
            }
            else
            {
                cart.Items.Add(new OrderItemViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductImageUrl = p.ImageUrl,
                    UnitPrice = p.Price,
                    Stock = p.Stock
                });
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = GetCartFromSession();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = GetCartFromSession();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                cart.Items.Remove(item);

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int productId, int stock)
        {
            var cart = GetCartFromSession();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (stock <= 0)
                    cart.Items.Remove(item);
                else
                    item.Stock = stock;
            }
            SaveCartSession(cart);
            return RedirectToAction("Index");
        }
    }
}