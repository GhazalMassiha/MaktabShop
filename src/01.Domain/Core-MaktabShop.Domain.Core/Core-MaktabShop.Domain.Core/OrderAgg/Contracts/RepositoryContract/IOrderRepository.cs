using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;

namespace Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract
{
    public interface IOrderRepository
    {
        Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken);
        Task<OrderDto?> GetLastOrderForUser(int userId, decimal totalPrice, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAllOrders(CancellationToken cancellationToken);
        Task<OrderDto?> GetOrderDetails(int orderId, CancellationToken cancellationToken);
    }
}
