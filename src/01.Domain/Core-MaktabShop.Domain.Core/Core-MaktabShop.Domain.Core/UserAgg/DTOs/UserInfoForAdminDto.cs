using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.Enums;

namespace Core_MaktabShop.Domain.Core.UserAgg.DTOs
{
    public class UserInfoForAdminDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Wallet { get; set; }
    }
}
