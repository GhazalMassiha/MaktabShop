using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.CategoryAgg.Entities;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Entities;

namespace Core_MaktabShop.Domain.Core.ProductAgg.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
