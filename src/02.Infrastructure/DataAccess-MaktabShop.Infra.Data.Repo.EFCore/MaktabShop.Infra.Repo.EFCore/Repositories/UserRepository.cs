using Core_MaktabShop.Domain.Core.UserAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.UserAgg.DTOs;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaktabShop.Infra.Repo.EFCore.Repositories
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<UserLoginDto?> Login(string username, string password, CancellationToken cancellationToken)
        {
            return _context.Users
                .AsNoTracking()
                .Where(u => u.Username == username && u.PasswordHash == password)
                .Select(u => new UserLoginDto
                {
                    Id = u.Id,
                    Username = u.Username

                })
                .FirstOrDefault();
        }

        public async Task<bool> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = userCreateDto.Username,
                PasswordHash = userCreateDto.PasswordHash,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Phone = userCreateDto.Phone,
                Address = userCreateDto.Address

            };

            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserDto?> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
               .Where(a => a.Username == username)
               .Select(a => new UserDto
               {
                   Username = a.Username
               })
               .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetUserWalletBalance(int userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Wallet)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UpdateUserWallet(int userId, decimal newAmount, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(w => w.Id == userId)
                .ExecuteUpdateAsync(w => w.SetProperty
                (w => w.Wallet, newAmount), cancellationToken) > 0;

        }
    }
}
