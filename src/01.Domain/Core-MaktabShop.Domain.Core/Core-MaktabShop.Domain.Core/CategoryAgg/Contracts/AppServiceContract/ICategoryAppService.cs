using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract
{
    public interface ICategoryAppService
    {
        Task<Result<CategoryDto?>> GetById(int id, CancellationToken cancellationToken);
        Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken);
        Task<Result<bool>> AddCategory(CategoryCreateDto dto, CancellationToken cancellationToken);
        Task<Result<bool>> UpdateCategory(int id, CategoryCreateDto dto, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteCategory(int id, CancellationToken cancellationToken);
    }
}
