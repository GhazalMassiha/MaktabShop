namespace Core_MaktabShop.Domain.Core.UserAgg.DTOs
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
