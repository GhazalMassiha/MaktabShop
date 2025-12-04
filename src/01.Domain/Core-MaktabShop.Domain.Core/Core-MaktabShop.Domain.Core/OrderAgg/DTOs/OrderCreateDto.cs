using Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.OrderAgg.DTOs
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public List<OrderItemCreateDto> OrederItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
