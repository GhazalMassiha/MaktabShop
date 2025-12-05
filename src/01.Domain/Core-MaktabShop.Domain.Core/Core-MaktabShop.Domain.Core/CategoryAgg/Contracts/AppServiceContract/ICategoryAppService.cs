using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract
{
    public interface ICategoryAppService
    {
        Task<Result<CategoryDto?>> GetById(int id, CancellationToken cancellationToken);
        Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken);
    }
}
