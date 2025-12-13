using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;
using Service_MaktabShop.Domain.Service.Services;

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

        public async Task<Result<bool>> AddCategory(CategoryCreateDto dto, CancellationToken cancellationToken)
        {
            var ok = await categoryService.Create(dto, cancellationToken);
            if (!ok) return Result<bool>.Failure("خطا در ایجاد دسته‌بندی.");

            return Result<bool>.Success("دسته‌بندی با موفقیت ایجاد شد.", true);
        }

        public async Task<Result<bool>> UpdateCategory(int id, CategoryCreateDto dto, CancellationToken cancellationToken)
        {
            var ok = await categoryService.Update(id, dto, cancellationToken);
            if (!ok) return Result<bool>.Failure("خطا در بروزرسانی دسته‌بندی.");

            return Result<bool>.Success("دسته‌بندی با موفقیت بروزرسانی شد.", true);
        }

        public async Task<Result<bool>> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var ok = await categoryService.Delete(id, cancellationToken);
            if (!ok) return Result<bool>.Failure("خطا در حذف دسته‌بندی.");

            return Result<bool>.Success("دسته‌بندی با موفقیت حذف شد.", true);
        }
    }
}
