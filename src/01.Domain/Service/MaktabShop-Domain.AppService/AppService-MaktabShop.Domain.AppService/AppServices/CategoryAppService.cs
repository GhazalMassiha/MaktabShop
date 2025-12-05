using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;

namespace AppService_MaktabShop.Domain.AppService.AppServices
{
    public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
    {
        public async Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await categoryService.GetAll(cancellationToken);
            if (categories == null)
                return Result<List<CategoryDto>>.Failure("خطا در بازیابی دسته بندی ها.");

            return Result<List<CategoryDto>>.Success("دسته بندی ها با موفقیت بازیابی شدند.", categories);
        }

        public async Task<Result<CategoryDto?>> GetById(int id, CancellationToken cancellationToken)
        {
            var category = await categoryService.GetById(id, cancellationToken);
            if (category == null)
                return Result<CategoryDto?>.Failure("خطا در بازیابی دسته بندی.");

            return Result<CategoryDto?>.Success("دسته بندی با موفقیت بازیابی شد.", category);
        }
    }
}
