using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;
using Core_MaktabShop.Domain.Core.CategoryAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.DTOs;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaktabShop.Infra.Repo.EFCore.Repositories
{
    public class CategoryRepository(AppDbContext _context) : ICategoryRepository
    {
        public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
        {

            return await _context.Categories.Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name

                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name

            }).ToListAsync(cancellationToken);
        }


        public async Task<bool> Create (CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = categoryCreateDto.Name,
            };

            await _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> Update (int categoryId, CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            var affectedRows = _context.Categories
                .Where(c => c.Id == categoryId)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.Name, categoryCreateDto.Name)
                );

            return await affectedRows > 0;
        }


        public async Task<bool> Delete (int categoryId, CancellationToken cancellationToken)
        {
            var result = _context.Categories.Where(c => c.Id == categoryId).ExecuteDeleteAsync();

            return await result > 0;
        }
    }
}
