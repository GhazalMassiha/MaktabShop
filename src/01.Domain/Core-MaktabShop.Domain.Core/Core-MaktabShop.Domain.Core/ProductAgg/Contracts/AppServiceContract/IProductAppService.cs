using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract
{
    public interface IProductAppService
    {
        Task<Result<ProductDto?>> GetById(int id, CancellationToken cancellationToken);
        Task<Result<List<ProductDto>>> GetProductByCategoryId(int categoryId, CancellationToken cancellationToken);
        Task<Result<List<ProductDto>>> GetAll(CancellationToken cancellationToken);
        Task<Result<int>> GetProductStockById(int productId, CancellationToken cancellationToken);
        Task<Result<bool>> UpdateProductStock(int productId, int newStockNum, CancellationToken cancellationToken);

    }
}
