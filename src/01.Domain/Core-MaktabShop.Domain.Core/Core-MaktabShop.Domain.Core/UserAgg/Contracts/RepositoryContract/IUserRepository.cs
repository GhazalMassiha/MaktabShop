using Core_MaktabShop.Domain.Core.UserAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.UserAgg.Contracts.RepositoryContract
{
    public interface IUserRepository
    {
        Task<UserLoginDto?> Login(string username, string password, CancellationToken cancellationToken);
        Task<bool> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken);
        Task<UserDto?> GetByUsername(string username, CancellationToken cancellationToken);
        Task<decimal> GetUserWalletBalance(int userId, CancellationToken cancellationToken);
        Task<bool> UpdateUserWallet(int userId, decimal newAmount, CancellationToken cancellationToken);
    }
}
