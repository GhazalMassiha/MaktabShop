using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;

namespace Service_MaktabShop.Domain.Service.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await categoryRepository.GetAll(cancellationToken);
        }

        public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await categoryRepository.GetById(id, cancellationToken);
        }
    }
}
