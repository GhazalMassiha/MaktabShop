using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;

namespace Service_MaktabShop.Domain.Service.Services
{
    public class OrderItemService(IOrderItemRepository orderItemRepository) : IOrderItemService
    {
        public async Task<bool> Add(List<OrderItemCreateDto> OrderItems, CancellationToken cancellationToken)
        {
            return await orderItemRepository.Add(OrderItems, cancellationToken);
        }
    }
}
