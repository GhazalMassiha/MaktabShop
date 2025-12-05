using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;

namespace MaktabShop.Infra.Repo.EFCore.Repositories
{
    public class OrderRepository(AppDbContext _context) : IOrderRepository
    {
        public async Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                UserId = orderCreateDto.UserId,
                TotalPrice = orderCreateDto.TotalPrice

            };

            await _context.Orders.AddAsync(order, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
