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


        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }


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
