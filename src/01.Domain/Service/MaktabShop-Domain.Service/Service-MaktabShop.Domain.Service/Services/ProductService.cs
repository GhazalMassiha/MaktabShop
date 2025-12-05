using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;

namespace Service_MaktabShop.Domain.Service.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<List<ProductDto>> GetAll(CancellationToken cancellationToken)
        {
            return await productRepository.GetAll(cancellationToken);
        }

        public async Task<ProductDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await productRepository.GetById(id, cancellationToken);
        }

        public async Task<List<ProductDto>> GetProductByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            return await productRepository.GetProductByCategoryId(categoryId, cancellationToken);
        }

        public async Task<int> GetProductStockById(int productId, CancellationToken cancellationToken)
        {
            return await productRepository.GetProductStockById(productId, cancellationToken);
        }

        public async Task<bool> UpdateProductStock(int productId, int newStockNum, CancellationToken cancellationToken)
        {
            return await productRepository.UpdateProductStock(productId, newStockNum, cancellationToken);
        }
    }
}
