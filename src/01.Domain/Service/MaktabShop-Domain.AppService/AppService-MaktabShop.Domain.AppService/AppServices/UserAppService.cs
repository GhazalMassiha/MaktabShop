using Core_MaktabShop.Domain.Core._common;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.DTOs;
using Service_MaktabShop.Domain.Service.Services;

namespace AppService_MaktabShop.Domain.AppService.AppServices
{
    public class UserAppService(IUserService userService) : IUserAppService
    {
        public async Task<Result<decimal>> GetUserWalletBalance(int userId, CancellationToken cancellationToken)
        {
            var wallet = await userService.GetUserWalletBalance(userId, cancellationToken);
            if (wallet == 0)
                return Result<decimal>.Failure("خطا در بازیابی کیف پول.");

            return Result<decimal>.Success("کیف پول با موفقیت بازیابی شد.", wallet);
        }

        public async Task<Result<UserLoginDto?>> Login(string username, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result<UserLoginDto?>.Failure("نام کاربری نمیتواند خالی باشد");

            if (username.Length < 3)
                return Result<UserLoginDto?>.Failure("نام کاربری نمیتواند کمتر از 3 کاراکتر باشد");

            if (string.IsNullOrWhiteSpace(password))
                return Result<UserLoginDto?>.Failure("رمز عبور نمیتواند خالی باشد");

            if (username.Length < 6)
                return Result<UserLoginDto?>.Failure("نام کاربری نمیتواند کمتر از 6 کاراکتر باشد");

            var user = await userService.Login(username , password, cancellationToken);
            if (user == null)
                return Result<UserLoginDto?>.Failure("نام کاربری یا رمز عبور اشتباه است.");

            return Result<UserLoginDto?>.Success("ورود با موفقیت انجام شد.", user);
        }

        public async Task<Result<bool>> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userCreateDto.Username) || string.IsNullOrWhiteSpace(userCreateDto.PasswordHash))
                return Result<bool>.Failure("نام کاربری و رمز عبور الزامی است.");

            if (userService.GetByUsername(userCreateDto.Username, cancellationToken) != null)
                return Result<bool>.Failure("این نام کاربری قبلاً ثبت شده است.");

            bool ok = await userService.Register(userCreateDto, cancellationToken);
            if (!ok)
                return Result<bool>.Failure("خطا در ثبت کاربر.");

            return Result<bool>.Success("کاربر با موفقیت ثبت شد.", true);
        }

        public async Task<Result<bool>> UpdateUserWallet(int userId, decimal itemPrice, CancellationToken cancellationToken)
        {
            var wallet = await userService.GetUserWalletBalance(userId, cancellationToken);
            if (wallet == 0)
                return Result<bool>.Failure("خطا در بازیابی کیف پول.");

            if (wallet < itemPrice)
                return Result<bool>.Failure("موجودی حساب شما کافی نمیباشد.");

            decimal newWalletBalance = wallet - itemPrice;

            var ok = await userService.UpdateUserWallet(userId, newWalletBalance, cancellationToken);
            if (!ok)
                return Result<bool>.Failure("خطا در عملیات کیف پول.");

            return Result<bool>.Success("عملیات کیف پول با موفقیت انجام شد.", true);
        }

        public async Task<Result<List<UserInfoForAdminDto>>> GetAll(CancellationToken cancellationToken)
        {
            var users = await userService.GetAll(cancellationToken);
            return Result<List<UserInfoForAdminDto>>.Success("کاربران با موفقیت بازیابی شدند.", users);
        }

        public async Task<Result<UserInfoForAdminDto?>> GetById(int id, CancellationToken cancellationToken)
        {
            var user = await userService.GetById(id, cancellationToken);
            if (user == null)
                return Result<UserInfoForAdminDto?>.Failure("کاربر یافت نشد.");

            return Result<UserInfoForAdminDto?>.Success("کاربر بازیابی شد.", user);
        }
    }
}
