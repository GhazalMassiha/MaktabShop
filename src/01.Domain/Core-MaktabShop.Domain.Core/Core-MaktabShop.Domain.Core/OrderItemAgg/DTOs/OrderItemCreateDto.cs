namespace Core_MaktabShop.Domain.Core.OrderItemAgg.DTOs
{
    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
    }
}
