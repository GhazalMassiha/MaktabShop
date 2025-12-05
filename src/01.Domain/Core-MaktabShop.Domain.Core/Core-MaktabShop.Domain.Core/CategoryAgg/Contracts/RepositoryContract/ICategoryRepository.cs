using Core_MaktabShop.Domain.Core.CategoryAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.RepositoryContract
{
    public interface ICategoryRepository
    {
        Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
    }
}
