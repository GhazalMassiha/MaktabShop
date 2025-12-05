using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.ServiceContract;

namespace AppService_MaktabShop.Domain.AppService.AppServices
{
    public class OrderItemAppService(IProductService productService, IOrderItemService orderItemService) : IOrderItemAppService
    {
        public async Task<Result<bool>> Add(List<OrderItemCreateDto> orderItems, CancellationToken cancellationToken)
        {
            foreach (var item in orderItems)
            {
                if (item.Count > await productService.GetProductStockById(item.ProductId, cancellationToken))
                {
                    return Result<bool>.Failure("موجودی کالا برای خرید شما کافی نمیباشد.");
                }

            }

            var result = await orderItemService.Add(orderItems, cancellationToken);
            if (result)
            {
                return Result<bool>.Success("کالا با موفقیت به سبد خرید اضافه شد.", true);
            }

            return Result<bool>.Failure("خطا در اضافه کردن کالا به سبد خرید.");
        }
    }
}
