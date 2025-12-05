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

            builder.HasData(
                 new Product
                 {
                     Id = 1,
                     Name = "هودی مشکی مردانه",
                     Description = "«هودی مشکی مردانه‌ای راحت و روزمره، با طراحی کلاهدار و فرم کلاسیک. مناسب برای روزهای خنک یا پاییز و زمستان، این هودی با ترکیب پنبه و پلی‌استر احساس نرم و گرم دارد. سبک اسپرت و شهری آن به‌راحتی با شلوار جین یا اسلش ست می‌شود.»",
                     Price = 2200000M,
                     Stock = 10,
                     ImageUrl = "/images/products/men-black-hoodie.jpg",
                     CategoryId = 1
                 },
                new Product
                {
                    Id = 2,
                    Name = "کت چرم مشکی مردانه",
                    Description = "«کت چرم مشکی مردانه با طراحی کلاسیک و رسمی ـ نیمه‌رسمی. ساخته‌شده از چرم با کیفیت، این کت ظاهری شکیل و مقاوم دارد و برای استایل‌های شهری، رسمی‑کژوال یا شب مناسب است. ترکیب آن با شلوار جین یا کت‌وشلوار به هر دو استایل جلوه خاصی می‌دهد.»",
                    Price = 8890000M,
                    Stock = 15,
                    ImageUrl = "/images/products/men-black-leather-jacket.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "سوییشرت خاکستری مردانه",
                    Description = "«سوییشرت خاکستری مردانه، سبک و راحت؛ انتخابی ایده‌آل برای استفاده روزمره و استایل‌های کژوال. با پارچه نرم و گرم، این سوییشرت مناسب فصل پاییز یا بهار خنک است و به راحتی با تی‌شرت و شلوار راحتی یا جین قابل ترکیب است.»",
                    Price = 1990000M,
                    Stock = 20,
                    ImageUrl = "/images/products/men-grey-sweatshirt.jpg",
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 4,
                    Name = "کت چرم مشکی زنانه",
                    Description = "«کت چرم زنانه مشکی با طراحی مدرن و شیک — ترکیب جذاب برای استایل کژوال یا خیابانی. این کت به‌واسطه فرم و رنگ مشکی با اکثر لباس‌ها ست می‌شود و برای فصل پاییز یا زمستان ـ با لایه‌بندی مناسب — انتخابی مطمئن است.»",
                    Price = 5750000M,
                    Stock = 18,
                    ImageUrl = "/images/products/women-black-leather-jacket.jpg",
                    CategoryId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "کت جیر قهوه ای زنانه",
                    Description = "«کت جیر زنانه با رنگ قهوه‌ای گرم و طراحی کلاسیک، مناسب کسانی که دنبال ظاهری طبیعی، گرم و سبُک هستند. این کت برای روزهای خنک پاییز یا بهار مناسب است و با دامن، شلوار یا جین شکل‌های مختلفی می‌گیرد.»",
                    Price = 2790000M,
                    Stock = 22,
                    ImageUrl = "/images/products/women-brown-jacket.jpg",
                    CategoryId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "کت خز سفید زنانه",
                    Description = "«کت سفید با بافت خز ـ انتخابی لوکس و خاص برای فصل سرما. این کت جلوه‌ی زمستانی دارد و می‌تواند نقطه تمرکز استایل شما باشد؛ مناسب برای مهمانی، بیرون رفتن یا خیابان‌گردی در هوای سرد.»",
                    Price = 7750000M,
                    Stock = 5,
                    ImageUrl = "/images/products/women-white-fur-coat.webp",
                    CategoryId = 2
                },
                new Product
                {
                    Id = 7,
                    Name = "آیتم تست",
                    Description = "«کت سفید با بافت خز ـ انتخابی لوکس و خاص برای فصل سرما. این کت جلوه‌ی زمستانی دارد و می‌تواند نقطه تمرکز استایل شما باشد؛ مناسب برای مهمانی، بیرون رفتن یا خیابان‌گردی در هوای سرد.»",
                    Price = 1000M,
                    Stock = 100,
                    ImageUrl = "/images/products/women-test.jpg",
                    CategoryId = 2
                }
                );
        }
    }
}
