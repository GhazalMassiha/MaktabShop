using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.ServiceContract;
using Service_MaktabShop.Domain.Service.Services;

namespace AppService_MaktabShop.Domain.AppService.AppServices
{
    public class OrderAppService(IOrderService orderService, IOrderItemService orderItemService, IUserService userService
            , IProductService productService) : IOrderAppService
    {
        public async Task<Result<OrderDto>> Create(OrderCreateDto orderDto, CancellationToken cancellationToken)
        {
            var wallet = await userService.GetUserWalletBalance(orderDto.UserId, cancellationToken);
            if (wallet < orderDto.TotalPrice)
                return Result<OrderDto>.Failure("موجودی حساب شما کافی نمی‌باشد.");

            var newWalletBalance = wallet - orderDto.TotalPrice;
            var walletOk = await userService.UpdateUserWallet(orderDto.UserId, newWalletBalance, cancellationToken);
            if (!walletOk)
                return Result<OrderDto>.Failure("خطا در بروزرسانی کیف پول.");


            var ok = await orderService.Create(orderDto, cancellationToken);
            if (!ok)
                return Result<OrderDto>.Failure("خطا در ثبت سفارش.");


            var savedOrder = await orderService.GetLastOrderForUser(orderDto.UserId, orderDto.TotalPrice, cancellationToken);
            if (savedOrder == null)
                return Result<OrderDto>.Failure("خطا در بازیابی سفارش ثبت‌شده.");


            foreach (var item in orderDto.OrederItems)
            {
                item.OrderId = savedOrder.Id;
            }


            var finalOrderOk = await orderItemService.Add(orderDto.OrederItems, cancellationToken);
            if (!finalOrderOk)
                return Result<OrderDto>.Failure("خطا در ثبت آیتم‌های سفارش.");


            foreach (var item in orderDto.OrederItems)
            {
                var currentStock = await productService.GetProductStockById(item.ProductId, cancellationToken);
                var newStock = currentStock - item.Count;

                var stockOk = await productService.UpdateProductStock(item.ProductId, newStock, cancellationToken);
                if (!stockOk)
                    return Result<OrderDto>.Failure("خطا در بروزرسانی موجودی کالا.");
            }


            savedOrder.OrderItems = orderDto.OrederItems
                .Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Count = i.Count,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.Count * i.UnitPrice,
                    CreatedAt = DateTime.Now
                })
                .ToList();

            return Result<OrderDto>.Success("سفارش با موفقیت ثبت شد.", savedOrder);
        }

        public async Task<Result<List<OrderDto>>> GetAllOrders(CancellationToken cancellationToken)
        {
            var orders = await orderService.GetAllOrders(cancellationToken);
            return Result<List<OrderDto>>.Success("سفارش‌ها با موفقیت بازیابی شدند.", orders);
        }

        public async Task<Result<OrderDto?>> GetOrderDetails(int orderId, CancellationToken cancellationToken)
        {
            var order = await orderService.GetOrderDetails(orderId, cancellationToken);
            if (order == null)
                return Result<OrderDto?>.Failure("سفارش یافت نشد.");

            return Result<OrderDto?>.Success("جزئیات سفارش بازیابی شد.", order);
        }

        public async Task<Result<DashboardDto>> GetDashboardStats(CancellationToken cancellationToken)
        {
            var products = await productService.GetAll(cancellationToken);
            var users = await userService.GetAll(cancellationToken);
            var orders = await orderService.GetAllOrders(cancellationToken);

            var model = new DashboardDto
            {
                TotalProducts = products.Count,
                TotalUsers = users.Count,
                TotalOrders = orders.Count,
                TotalSales = orders.Sum(o => o.TotalPrice),
            };


            var filteredOrders = orders
                .Where(o => o.CreatedAt > DateTime.MinValue)
                .ToList();

            var grouped = filteredOrders
                .GroupBy(o => o.CreatedAt.Date)
                .OrderBy(g => g.Key)
                .Select(g => new {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                })
                .ToList();

            foreach (var g in grouped)
            {
                model.OrderDates.Add(g.Date);
                model.OrdersPerDay.Add(g.Count);
            }

            return Result<DashboardDto>.Success("آمار داشبورد بارگذاری شد.", model);
        }
    }
}
