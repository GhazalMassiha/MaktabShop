using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.DTOs;
using Service_MaktabShop.Domain.Service.Services;

namespace AppService_MaktabShop.Domain.AppService.AppServices
{
    public class ProductAppService(IProductService productService) : IProductAppService
    {
        public async Task<Result<List<ProductDto>>> GetAll(CancellationToken cancellationToken)
        {
            var products = await productService.GetAll(cancellationToken);
            if (products == null)
                return Result<List<ProductDto>>.Failure("خطا در بازیابی کالاها.");

            return Result<List<ProductDto>>.Success("کالاها با موفقیت بازیابی شدند.", products);
        }

        public async Task<Result<ProductDto?>> GetById(int id, CancellationToken cancellationToken)
        {
            var product = await productService.GetById(id, cancellationToken);
            if (product == null)
                return Result<ProductDto?>.Failure("خطا در بازیابی کالا.");

            return Result<ProductDto?>.Success("کالا با موفقیت بازیابی شد.", product);
        }

        public async Task<Result<List<ProductDto>>> GetProductByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var products = await productService.GetProductByCategoryId(categoryId, cancellationToken);
            if (products == null)
                return Result<List<ProductDto>>.Failure("خطا در بازیابی کالاها.");

            return Result<List<ProductDto>>.Success("کالاها با موفقیت بازیابی شدند.", products);
        }

        public async Task<Result<int>> GetProductStockById(int productId, CancellationToken cancellationToken)
        {
            var stock = await productService.GetProductStockById(productId, cancellationToken);
            if (stock == 0)
                return Result<int>.Failure("خطا در بازیابی موجودی کالا.");

            return Result<int>.Success("موجودی کالا با موفقیت بازیابی شد.", stock);
        }

        public async Task<Result<bool>> UpdateProductStock(int productId, int newStockNum, CancellationToken cancellationToken)
        {
            var product = await productService.GetById(productId, cancellationToken);
            if (product == null)
                return Result<bool>.Failure("خطا در بازیابی کالا.");

            var ok = await productService.UpdateProductStock(productId, newStockNum, cancellationToken);
            if (!ok)
                return Result<bool>.Failure("خطا در بروزرسانی موجودی کالا.");

            return Result<bool>.Success("موجودی کالا با موفقیت بروزرسانی شد.", true);
        }

        public async Task<Result<bool>> AddProduct(ProductCreateDto dto, CancellationToken cancellationToken)
        {
            var ok = await productService.AddProduct(dto, cancellationToken);
            if (!ok)
                return Result<bool>.Failure("خطا در ایجاد کالا.");

            return Result<bool>.Success("کالا با موفقیت ایجاد شد.", true);
        }

        public async Task<Result<bool>> UpdateProduct(int id, ProductCreateDto dto, CancellationToken cancellationToken)
        {
            var ok = await productService.UpdateProduct(id, dto, cancellationToken);
            if (!ok)
                return Result<bool>.Failure("خطا در بروزرسانی کالا.");

            return Result<bool>.Success("کالا با موفقیت بروزرسانی شد.", true);
        }

        public async Task<Result<bool>> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var ok = await productService.DeleteProduct(id, cancellationToken);
            if (!ok)
                return Result<bool>.Failure("خطا در حذف کالا.");

            return Result<bool>.Success("کالا با موفقیت حذف شد.", true);
        }
    }
}
