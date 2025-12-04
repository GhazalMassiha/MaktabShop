using Core_MaktabShop.Domain.Core.OrderItemAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabShop.Infra.SqlServer.EFCore.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(o => o.Count);

            builder.Property(o => o.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Product)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
