using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;

namespace Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract
{
    public interface IOrderRepository
    {
        Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken);
    }
}
