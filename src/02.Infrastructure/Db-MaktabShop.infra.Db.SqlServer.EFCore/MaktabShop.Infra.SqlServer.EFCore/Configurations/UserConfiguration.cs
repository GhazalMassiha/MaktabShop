using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabShop.Infra.SqlServer.EFCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Wallet)
                .HasColumnType("decimal(18,2)");


            builder.Property(u => u.Role)
                      .HasConversion<string>()
                      .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasMany(o => o.Orders)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                 new User
                 {
                     Id = 1,
                     Username = "ghazal",
                     PasswordHash = "123456",   
                     FirstName = "غزل",
                     LastName = "مسیحا",
                     Phone = "09123456789",
                     Address = "تهران، شهرک غرب",
                     Wallet = 10000000M,
                     Role = Core_MaktabShop.Domain.Core.UserAgg.Enums.RoleEnum.NormaUser
                 },
                new User
                {
                    Id = 2,
                    Username = "mersedeh",
                    PasswordHash = "123456",
                    FirstName = "مرسده",
                    LastName = "کسروی",
                    Phone = "09111223344",
                    Address = "رشت، گلسار",
                    Wallet = 7000000,
                    Role = Core_MaktabShop.Domain.Core.UserAgg.Enums.RoleEnum.NormaUser
                },
                new User
                {
                    Id = 3,
                    Username = "amir",
                    PasswordHash = "123456",
                    FirstName = "امیر",
                    LastName = "ساعدی نیا",
                    Phone = "09111223343",
                    Address = "تهران، پونک",
                    Wallet = 5000000M,
                    Role = Core_MaktabShop.Domain.Core.UserAgg.Enums.RoleEnum.NormaUser
                },
                new User
                {
                    Id = 4,
                    Username = "admin",
                    PasswordHash = "123456",
                    FirstName = "ادمین",
                    LastName = "1",
                    Phone = "09111223342",
                    Address = "تهران",
                    Wallet = 100000000M,
                    Role = Core_MaktabShop.Domain.Core.UserAgg.Enums.RoleEnum.Admin
                }
                );

        }
    }
}
