using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabShop.Infra.SqlServer.EFCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
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


            //builder.Property(u => u.Role)
            //          .HasConversion<string>()
            //          .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasMany(o => o.Orders)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            var hasher = new PasswordHasher<AppUser>();

            builder.HasData(
                 new AppUser
                 {
                     Id = 1,
                     UserName = "ghazal",
                     NormalizedUserName = "GHAZAL",
                     PasswordHash = hasher.HashPassword(null, "123456"),
                     FirstName = "غزل",
                     LastName = "مسیحا",
                     PhoneNumber = "09123456789",
                     Address = "تهران، شهرک غرب",
                     Wallet = 10000000M,
                     SecurityStamp = Guid.NewGuid().ToString("D"),
                     ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                     LockoutEnabled = false,
                 },
                new AppUser
                {
                    Id = 2,
                    UserName = "mersedeh",
                    NormalizedUserName = "MERSEDEH",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    FirstName = "مرسده",
                    LastName = "کسروی",
                    PhoneNumber = "09111223344",
                    Address = "رشت، گلسار",
                    Wallet = 7000000,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    LockoutEnabled = false,
                },
                new AppUser
                {
                    Id = 3,
                    UserName = "amir",
                    NormalizedUserName = "AMIR",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    FirstName = "امیر",
                    LastName = "ساعدی نیا",
                    PhoneNumber = "09111223343",
                    Address = "تهران، پونک",
                    Wallet = 5000000M,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    LockoutEnabled = false,
                },
                new AppUser
                {
                    Id = 4,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    FirstName = "ادمین",
                    LastName = "1",
                    PhoneNumber = "09111223342",
                    Address = "تهران",
                    Wallet = 100000000M,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    LockoutEnabled = false,
                }
                );

        }
    }
}
