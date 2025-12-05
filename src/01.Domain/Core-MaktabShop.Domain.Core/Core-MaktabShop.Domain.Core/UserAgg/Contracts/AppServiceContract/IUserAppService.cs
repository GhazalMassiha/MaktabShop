using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.UserAgg.DTOs;

namespace Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract
{
    public interface IUserAppService
    {
        Task<Result<UserLoginDto?>> Login(string username, string password, CancellationToken cancellationToken);
        Task<Result<bool>> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken);
        Task<Result<decimal>> GetUserWalletBalance(int userId, CancellationToken cancellationToken);
        Task<Result<bool>> UpdateUserWallet(int userId, decimal itemPrice, CancellationToken cancellationToken);
    }
}
