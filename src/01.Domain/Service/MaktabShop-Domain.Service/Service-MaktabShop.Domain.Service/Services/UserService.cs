using Core_MaktabShop.Domain.Core.UserAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.DTOs;

namespace Service_MaktabShop.Domain.Service.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<decimal> GetUserWalletBalance(int userId, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserWalletBalance(userId, cancellationToken);
        }

        public async Task<UserLoginDto?> Login(string username, string password, CancellationToken cancellationToken)
        {
            return await userRepository.Login(username, password, cancellationToken);
        }

        public async Task<UserDto?> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return await userRepository.GetByUsername(username, cancellationToken);
        }

        public async Task<bool> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            return await userRepository.Register(userCreateDto, cancellationToken);
        }

        public async Task<bool> UpdateUserWallet(int userId, decimal newAmount, CancellationToken cancellationToken)
        {
            return await userRepository.UpdateUserWallet(userId, newAmount, cancellationToken);
        }

        public async Task<List<UserInfoForAdminDto>> GetAll(CancellationToken cancellationToken)
        {
            return await userRepository.GetAll(cancellationToken);
        }

        public async Task<UserInfoForAdminDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await userRepository.GetById(id, cancellationToken);
        }

        public async Task<UserInfoForAdminDto?> GetByUsernameForAdmin(string username, CancellationToken cancellationToken)
        {
            return await userRepository.GetByUsernameForAdmin(username, cancellationToken);
        }
    }
}
