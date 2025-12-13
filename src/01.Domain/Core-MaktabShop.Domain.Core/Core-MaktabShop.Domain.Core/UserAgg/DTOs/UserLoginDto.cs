using Core_MaktabShop.Domain.Core.UserAgg.Enums;

namespace Core_MaktabShop.Domain.Core.UserAgg.DTOs
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public RoleEnum Role { get; set; }
    }
}
