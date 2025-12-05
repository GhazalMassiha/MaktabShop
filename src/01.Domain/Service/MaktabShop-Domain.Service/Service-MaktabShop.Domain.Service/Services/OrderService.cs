using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;

namespace Service_MaktabShop.Domain.Service.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
            return await orderRepository.Create(orderCreateDto, cancellationToken);
        }
    }
}
