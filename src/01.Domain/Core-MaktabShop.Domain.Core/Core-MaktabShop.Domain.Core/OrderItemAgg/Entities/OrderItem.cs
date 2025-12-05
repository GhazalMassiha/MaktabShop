using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using Core_MaktabShop.Domain.Core.ProductAgg.Entities;

namespace Core_MaktabShop.Domain.Core.OrderItemAgg.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; private set; }

        private void CalculateTotalPrice ()
        {
            TotalPrice = Count * UnitPrice;
        }
    }
}
