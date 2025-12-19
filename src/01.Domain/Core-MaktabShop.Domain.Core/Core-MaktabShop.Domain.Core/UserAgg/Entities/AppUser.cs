using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core_MaktabShop.Domain.Core.UserAgg.Entities
{
    public class AppUser : IdentityUser<int>
    {
        //public string Username { get; set; }
        //public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Wallet { get; set; }

        //public RoleEnum Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
