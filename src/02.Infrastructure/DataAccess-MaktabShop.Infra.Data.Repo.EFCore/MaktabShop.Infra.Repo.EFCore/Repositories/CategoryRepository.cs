using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;
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
    }
}
