using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;

namespace Service_MaktabShop.Domain.Service.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
            return await orderRepository.Create(orderCreateDto, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await orderRepository.GetAllOrders(cancellationToken);
        }

        public async Task<OrderDto?> GetLastOrderForUser(int userId, decimal totalPrice, CancellationToken cancellationToken)
        {
            return await orderRepository.GetLastOrderForUser(userId, totalPrice, cancellationToken);
        }

        public async Task<OrderDto?> GetOrderDetails(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.GetOrderDetails(orderId, cancellationToken);
        }
    }
}
