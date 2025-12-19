using Core_MaktabShop.Domain.Core.OrderAgg.Entities;

namespace Core_MaktabShop.Domain.Core.UserAgg.DTOs
{
    public class UserEditDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Wallet { get; set; }
    }
}
