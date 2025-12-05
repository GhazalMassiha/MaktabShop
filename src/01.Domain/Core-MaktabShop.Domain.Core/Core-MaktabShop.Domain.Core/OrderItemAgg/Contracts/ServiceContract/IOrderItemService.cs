using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.ServiceContract
{
    public interface IOrderItemService
    {
        Task<bool> Add(List<OrderItemCreateDto> OrderItems, CancellationToken cancellationToken);
    }
}
