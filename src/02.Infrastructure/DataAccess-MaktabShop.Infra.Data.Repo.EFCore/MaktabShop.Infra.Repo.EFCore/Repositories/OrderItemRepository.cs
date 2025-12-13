using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Entities;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;

namespace MaktabShop.Infra.Repo.EFCore.Repositories
{
    public class OrderItemRepository(AppDbContext context) : IOrderItemRepository
    {
        public async Task<bool> Add(List<OrderItemCreateDto> OrderItems, CancellationToken cancellationToken)
        {
            var orderItem = OrderItems.Select(o => new OrderItem
            {
                ProductId = o.ProductId,
                OrderId = o.OrderId, 
                Count = o.Count,
                UnitPrice = o.UnitPrice

            }).ToList();

            await context.OrderItems.AddRangeAsync(orderItem, cancellationToken);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
