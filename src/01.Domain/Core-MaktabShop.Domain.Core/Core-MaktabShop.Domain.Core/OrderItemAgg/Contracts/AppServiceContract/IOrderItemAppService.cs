using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.AppServiceContract
{
    public interface IOrderItemAppService
    {
        Task<Result<bool>> Add(List<OrderItemCreateDto> orderItems, CancellationToken cancellationToken);
    }
}
