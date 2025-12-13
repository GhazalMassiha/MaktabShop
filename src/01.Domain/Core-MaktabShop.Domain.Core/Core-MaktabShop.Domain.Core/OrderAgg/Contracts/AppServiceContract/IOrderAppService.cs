using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract
{
    public interface IOrderAppService
    {
        Task<Result<OrderDto>> Create(OrderCreateDto orderDto, CancellationToken cancellationToken);
        Task<Result<List<OrderDto>>> GetAllOrders(CancellationToken cancellationToken);
        Task<Result<OrderDto?>> GetOrderDetails(int orderId, CancellationToken cancellationToken);
        Task<Result<DashboardDto>> GetDashboardStats(CancellationToken cancellationToken);
    }
}
