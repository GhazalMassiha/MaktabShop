using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;
using Core_MaktabShop.Domain.Core.ProductAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
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

        public async Task<bool> AddProduct(ProductCreateDto dto, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            };

            await _context.Products.AddAsync(entity, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateProduct(int id, ProductCreateDto dto, CancellationToken cancellationToken)
        {
            var affectedRows = _context.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.Name, dto.Name)
                .SetProperty(p => p.Description, dto.Description)
                .SetProperty(p => p.Price, dto.Price)
                .SetProperty(p => p.Stock, dto.Price)
                .SetProperty(p => p.ImageUrl, dto.ImageUrl)
                .SetProperty(p => p.CategoryId, dto.CategoryId)
                );

            return await affectedRows > 0;
        }

        public async Task<bool> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var result = _context.Products.Where(c => c.Id == id).ExecuteDeleteAsync();

            return await result > 0;
        }

    }
}
