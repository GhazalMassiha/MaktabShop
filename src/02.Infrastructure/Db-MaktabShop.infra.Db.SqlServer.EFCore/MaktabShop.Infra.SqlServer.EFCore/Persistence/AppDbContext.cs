using Core_MaktabShop.Domain.Core.CategoryAgg.Entities;
using Core_MaktabShop.Domain.Core.OrderAgg.Entities;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Entities;
using Core_MaktabShop.Domain.Core.ProductAgg.Entities;
using Core_MaktabShop.Domain.Core.UserAgg.Entities;
using MaktabShop.Infra.SqlServer.EFCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MaktabShop.Infra.SqlServer.EFCore.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }


        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
