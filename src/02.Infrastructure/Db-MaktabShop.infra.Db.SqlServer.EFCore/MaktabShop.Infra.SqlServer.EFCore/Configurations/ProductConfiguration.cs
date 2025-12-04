using Core_MaktabShop.Domain.Core.ProductAgg.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabShop.Infra.SqlServer.EFCore.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(u => u.CreatedAt)
               .HasDefaultValueSql("GetDate()")
               .ValueGeneratedOnAdd();
        }
    }
}
