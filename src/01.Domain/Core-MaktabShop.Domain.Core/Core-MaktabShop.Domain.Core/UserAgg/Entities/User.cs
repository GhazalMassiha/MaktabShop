using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;

namespace Core_MaktabShop.Domain.Core.UserAgg.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public decimal Wallet { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
