using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;

namespace Core_MaktabShop.Domain.Core.OrderAgg.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
