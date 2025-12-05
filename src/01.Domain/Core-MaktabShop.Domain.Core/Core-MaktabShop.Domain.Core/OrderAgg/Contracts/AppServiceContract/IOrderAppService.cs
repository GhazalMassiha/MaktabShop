using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract
{
    public interface IOrderAppService
    {
        Task<Result<bool>> Create(OrderCreateDto orderDto, CancellationToken cancellationToken);
        Task<Result<bool>> WalletOperationToOrder(OrderCreateDto orderDto, CancellationToken cancellationToken);
    }
}
