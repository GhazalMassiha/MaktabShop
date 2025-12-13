using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using MaktabShop_EndPoimt.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MaktabShop_EndPoimt.MVC.Controllers
{
    public class CartController(IProductAppService productAppService, ILogger<CartController> logger) : Controller
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
            logger.LogInformation("درخواست افزودن به سبد خرید محصول {ProductId} با تعداد {Count}", productId, stock);

            var prodRes = await productAppService.GetById(productId, CancellationToken.None);
            if (!prodRes.IsSuccess || prodRes.Data == null)
            {
                logger.LogWarning("محصول {ProductId} یافت نشد", productId);
                return NotFound();
            }

            var cart = GetCartFromSession();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Stock += stock;
                logger.LogInformation("محصول {ProductId} در سبد خرید افزایش یافت به {NewStock}", productId, existing.Stock);
            }
            else
            {
                cart.Items.Add(new OrderItemViewModel
                {
                    ProductId = prodRes.Data.Id,
                    ProductName = prodRes.Data.Name,
                    ProductImageUrl = prodRes.Data.ImageUrl,
                    UnitPrice = prodRes.Data.Price,
                    Stock = stock

                });
                logger.LogInformation("محصول {ProductId} به سبد خرید اضافه شد", productId);
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            logger.LogInformation("نمایش سبد خرید");
            var cart = GetCartFromSession();

            return View(cart);
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            logger.LogInformation("درخواست حذف محصول {ProductId} از سبد خرید", productId);

            var cart = GetCartFromSession();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                logger.LogInformation("محصول {ProductId} از سبد خرید حذف شد", productId);
            }

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int productId, int stock)
        {
            logger.LogInformation("درخواست به‌روزرسانی تعداد محصول {ProductId} به {Stock}", productId, stock);

            var cart = GetCartFromSession();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (stock <= 0)
                {
                    cart.Items.Remove(item);
                    logger.LogWarning("تعداد {Stock} نامعتبر برای محصول {ProductId}؛ حذف شد", stock, productId);
                }
                else
                {
                    item.Stock = stock;
                    logger.LogInformation("موجودی محصول {ProductId} در سبد خرید به {Stock} تغییر یافت", productId, stock);
                }
            }

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }
    }
}