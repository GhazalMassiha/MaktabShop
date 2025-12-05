using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaktabShop.Infra.SqlServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DeletedAt", "Name", "UptatedAt" },
                values: new object[,]
                {
                    { 1, null, "مردانه", null },
                    { 2, null, "زنانه", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DeletedAt", "FirstName", "LastName", "PasswordHash", "Phone", "UptatedAt", "Username", "Wallet" },
                values: new object[,]
                {
                    { 1, "تهران، شهرک غرب", null, "غزل", "مسیحا", "123456", "09123456789", null, "ghazal", 10000000m },
                    { 2, "رشت، گلسار", null, "مرسده", "کسروی", "123456", "09111223344", null, "mersedeh", 7000000m },
                    { 3, "تهران، پونک", null, "امیر", "ساعدی نیا", "123456", "09111223343", null, "amir", 5000000m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DeletedAt", "Description", "ImageUrl", "Name", "Price", "Stock", "UptatedAt" },
                values: new object[,]
                {
                    { 1, 1, null, "«هودی مشکی مردانه‌ای راحت و روزمره، با طراحی کلاهدار و فرم کلاسیک. مناسب برای روزهای خنک یا پاییز و زمستان، این هودی با ترکیب پنبه و پلی‌استر احساس نرم و گرم دارد. سبک اسپرت و شهری آن به‌راحتی با شلوار جین یا اسلش ست می‌شود.»", "/images/products/men-black-hoodie.jpg", "هودی مشکی مردانه", 2200000m, 10, null },
                    { 2, 1, null, "«کت چرم مشکی مردانه با طراحی کلاسیک و رسمی ـ نیمه‌رسمی. ساخته‌شده از چرم با کیفیت، این کت ظاهری شکیل و مقاوم دارد و برای استایل‌های شهری، رسمی‑کژوال یا شب مناسب است. ترکیب آن با شلوار جین یا کت‌وشلوار به هر دو استایل جلوه خاصی می‌دهد.»", "/images/products/men-black-leather-jacket.jpg", "کت چرم مشکی مردانه", 8890000m, 15, null },
                    { 3, 1, null, "«سوییشرت خاکستری مردانه، سبک و راحت؛ انتخابی ایده‌آل برای استفاده روزمره و استایل‌های کژوال. با پارچه نرم و گرم، این سوییشرت مناسب فصل پاییز یا بهار خنک است و به راحتی با تی‌شرت و شلوار راحتی یا جین قابل ترکیب است.»", "/images/products/men-grey-sweatshirt.jpg", "سوییشرت خاکستری مردانه", 1990000m, 20, null },
                    { 4, 2, null, "«کت چرم زنانه مشکی با طراحی مدرن و شیک — ترکیب جذاب برای استایل کژوال یا خیابانی. این کت به‌واسطه فرم و رنگ مشکی با اکثر لباس‌ها ست می‌شود و برای فصل پاییز یا زمستان ـ با لایه‌بندی مناسب — انتخابی مطمئن است.»", "/images/products/women-black-leather-jacket.jpg", "کت چرم مشکی زنانه", 5750000m, 18, null },
                    { 5, 2, null, "«کت جیر زنانه با رنگ قهوه‌ای گرم و طراحی کلاسیک، مناسب کسانی که دنبال ظاهری طبیعی، گرم و سبُک هستند. این کت برای روزهای خنک پاییز یا بهار مناسب است و با دامن، شلوار یا جین شکل‌های مختلفی می‌گیرد.»", "/images/products/women-brown-jacket.jpg", "کت جیر قهوه ای زنانه", 2790000m, 22, null },
                    { 6, 2, null, "«کت سفید با بافت خز ـ انتخابی لوکس و خاص برای فصل سرما. این کت جلوه‌ی زمستانی دارد و می‌تواند نقطه تمرکز استایل شما باشد؛ مناسب برای مهمانی، بیرون رفتن یا خیابان‌گردی در هوای سرد.»", "/images/products/women-white-fur-coat.jpg", "کت خز سفید زنانه", 7750000m, 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
