using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.ServiceContract;

namespace AppService_MaktabShop.Domain.AppService.AppServices
{
    public class OrderAppService(IOrderService orderService, IOrderItemService orderItemService, IUserService userService
            , IProductService productService) : IOrderAppService
    {
        public async Task<Result<bool>> WalletOperationToOrder(OrderCreateDto orderDto, CancellationToken cancellationToken)
        {
            var wallet = await userService.GetUserWalletBalance(orderDto.UserId, cancellationToken);
            if (wallet < orderDto.TotalPrice)
            {
                return Result<bool>.Failure("موجودی حساب شما کافی نمیباشد.");
            }

            var newWalletBalance = wallet - orderDto.TotalPrice;
            var walletDeductionResult = await userService.UpdateUserWallet(orderDto.UserId, newWalletBalance, cancellationToken);
            if (!walletDeductionResult)
                return Result<bool>.Failure("خطا در انجام عملیات کیف پول.");

            return Result<bool>.Success("عملیات کیف پول با موفقیت انجام شد.", true);
        }

        public async Task<Result<bool>> Create(OrderCreateDto orderDto, CancellationToken cancellationToken)
        {

            await WalletOperationToOrder(orderDto, cancellationToken);

            var ok = await orderService.Create(orderDto, cancellationToken);
            if (!ok)
            {
                return Result<bool>.Failure("خطا در ثبت سفارش.");
            }

            foreach (var item in orderDto.OrederItems)
            {
                item.OrderId = orderDto.UserId;

                var newProductStock = await productService.GetProductStockById(item.ProductId, cancellationToken) - item.Count;

                var stockOk = await productService.UpdateProductStock(item.ProductId, newProductStock, cancellationToken);
                if (!stockOk)
                {
                    return Result<bool>.Failure("خطا در بروزرسانی موجودی کالا.");
                }

            }

            var finalOrderOk = await orderItemService.Add(orderDto.OrederItems, cancellationToken);
            if (!finalOrderOk)
            {
                return Result<bool>.Failure("خطا در ثبت نهایی سفارش.");
            }

            return Result<bool>.Success("سفارش شما با موفقیت پرداخت و ثبت نهایی شد.", true);
        }
    }
}
