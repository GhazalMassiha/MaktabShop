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

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasMany(o => o.Orders)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
