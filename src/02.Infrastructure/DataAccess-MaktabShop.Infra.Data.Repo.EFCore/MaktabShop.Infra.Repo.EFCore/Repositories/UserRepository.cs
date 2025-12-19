using Core_MaktabShop.Domain.Core.UserAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.UserAgg.DTOs;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaktabShop.Infra.Repo.EFCore.Repositories
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<List<UserInfoForAdminDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Select(u => new UserInfoForAdminDto()
                {
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Phone = u.PhoneNumber,
                    Wallet = u.Wallet
                })
                .ToListAsync();
        }


        public async Task<UserInfoForAdminDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => new UserInfoForAdminDto
                {
                    Id = u.Id,
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Phone = u.PhoneNumber,
                    Wallet = u.Wallet
                })
                .FirstOrDefaultAsync();
        }


        public async Task<UserInfoForAdminDto?> GetByUsernameForAdmin(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.UserName == username)
                .Select(u => new UserInfoForAdminDto
                {
                    Id = u.Id,
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Phone = u.PhoneNumber,
                    Wallet = u.Wallet
                })
                .FirstOrDefaultAsync();
        }


        public async Task<UserLoginDto?> Login(string username, string password, CancellationToken cancellationToken)
        {
            return _context.Users
                .AsNoTracking()
                .Where(u => u.UserName == username && u.PasswordHash == password)
                .Select(u => new UserLoginDto
                {
                    Id = u.Id,
                    Username = u.UserName,
                    //Role = u.Role

                })
                .FirstOrDefault();
        }

        public async Task<bool> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                UserName = userCreateDto.Username,
                PasswordHash = userCreateDto.PasswordHash,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                PhoneNumber = userCreateDto.Phone,
                Address = userCreateDto.Address

            };

            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserDto?> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
               .Where(a => a.UserName == username)
               .Select(a => new UserDto
               {
                   Username = a.UserName
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


        public async Task<bool> Update(int userId, UserEditDto dto, CancellationToken cancellationToken)
        {
            var affectedRows = _context.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(u => u.UserName, dto.UserName)
                .SetProperty(u => u.PasswordHash, dto.Password)
                .SetProperty(u => u.FirstName, dto.FirstName)
                .SetProperty(u => u.LastName, dto.LastName)
                .SetProperty(u => u.PhoneNumber, dto.PhoneNumber)
                .SetProperty(u => u.Address, dto.Address)
                .SetProperty(u => u.Wallet, dto.Wallet)
                );


            return await affectedRows > 0;
        }
    }
}
