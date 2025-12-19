using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using Core_MaktabShop.Domain.Core.ProductAgg.Entities;

namespace Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
