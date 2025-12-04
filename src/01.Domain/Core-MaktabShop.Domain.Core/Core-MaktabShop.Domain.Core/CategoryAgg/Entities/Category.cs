using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.ProductAgg.Entities;

namespace Core_MaktabShop.Domain.Core.CategoryAgg.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
