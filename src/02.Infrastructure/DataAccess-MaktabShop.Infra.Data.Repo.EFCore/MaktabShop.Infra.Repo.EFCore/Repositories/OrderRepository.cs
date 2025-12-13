using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderAgg.DTOs;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<OrderDto>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await _context.Orders
           .Select(o => new OrderDto
           {
               Id = o.Id,
               UserId = o.UserId,
               TotalPrice = o.TotalPrice,
               OrderItems = new List<OrderItemDto>()

           }).ToListAsync(cancellationToken);
        }

        public async Task<OrderDto?> GetLastOrderForUser(int userId, decimal totalPrice, CancellationToken cancellationToken)
        {
            return await _context.Orders
                    .Where(o => o.UserId == userId && o.TotalPrice == totalPrice)
                    .OrderByDescending(o => o.Id)
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        UserId = o.UserId,
                        TotalPrice = o.TotalPrice,
                        OrderItems = new List<OrderItemDto>()
                    })
                    .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<OrderDto?> GetOrderDetails(int orderId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
            .Where(o => o.Id == orderId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                TotalPrice = o.TotalPrice,
                OrderItems = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice,
                    Count = i.Count,
                    ProductImageUrl = i.Product.ImageUrl
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

            return order;
        }
    }
}
