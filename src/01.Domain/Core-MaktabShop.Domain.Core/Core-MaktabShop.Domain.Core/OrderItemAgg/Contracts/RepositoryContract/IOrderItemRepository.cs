using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.RepositoryContract
{
    public interface IOrderItemRepository
    {
        Task<bool> Add(List<OrderItemCreateDto> OrderItems, CancellationToken cancellationToken);
    }
}
