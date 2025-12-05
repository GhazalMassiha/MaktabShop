using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.ProductAgg.Contracts.RepositoryContract
{
    public interface IProductRepository
    {
        Task<ProductDto?> GetById(int id, CancellationToken cancellationToken);
        Task<List<ProductDto>> GetProductByCategoryId(int categoryId, CancellationToken cancellationToken);
        Task<List<ProductDto>> GetAll(CancellationToken cancellationToken);
        Task<int> GetProductStockById(int productId, CancellationToken cancellationToken);
        Task<bool> UpdateProductStock(int productId, int newStockNum, CancellationToken cancellationToken);
    }
}
