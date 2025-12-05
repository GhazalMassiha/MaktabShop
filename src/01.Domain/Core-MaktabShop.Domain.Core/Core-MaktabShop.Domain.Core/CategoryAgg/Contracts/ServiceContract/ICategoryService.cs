using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.ServiceContract
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
    }
}
