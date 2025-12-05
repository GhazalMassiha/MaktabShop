using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaktabShop.Infra.Repo.EFCore.Repositories
{
    public class ProductRepository(AppDbContext _context) : IProductRepository
    {

        public async Task<ProductDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl

                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ProductDto>> GetProductByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl

                }).ToListAsync(cancellationToken);
        }

        public async Task<List<ProductDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl

                }).ToListAsync(cancellationToken);
        }

        public async Task<int> GetProductStockById(int productId, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Stock)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UpdateProductStock(int productId, int newStockNum, CancellationToken cancellationToken)
        {

            return await _context.Products
                .Where(p => p.Id == productId)
                .ExecuteUpdateAsync(p => p.SetProperty(p => p.Stock, newStockNum), cancellationToken) > 0;
        }

    }
}
