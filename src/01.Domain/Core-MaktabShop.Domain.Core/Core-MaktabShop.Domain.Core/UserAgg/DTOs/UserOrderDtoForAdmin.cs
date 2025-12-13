using Core_MaktabShop.Domain.Core.OrderAgg.Entities;

namespace Core_MaktabShop.Domain.Core.UserAgg.DTOs
{
    public class UserOrderDtoForAdmin
    {
        public int Id { get; set; }
        public int Username { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
