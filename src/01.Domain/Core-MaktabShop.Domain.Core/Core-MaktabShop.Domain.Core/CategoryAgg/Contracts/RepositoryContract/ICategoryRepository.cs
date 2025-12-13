using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;
using Core_MaktabShop.Domain.Core.CategoryAgg.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.RepositoryContract
{
    public interface ICategoryRepository
    {
        Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<bool> Create(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(int categoryId, CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int categoryId, CancellationToken cancellationToken);
    }
}
