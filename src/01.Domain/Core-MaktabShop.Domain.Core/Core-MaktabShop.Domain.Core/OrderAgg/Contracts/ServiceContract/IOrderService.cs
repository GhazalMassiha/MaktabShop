using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderAgg.Contracts.ServiceContract
{
    public interface IOrderService
    {
        Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken);
    }
}
