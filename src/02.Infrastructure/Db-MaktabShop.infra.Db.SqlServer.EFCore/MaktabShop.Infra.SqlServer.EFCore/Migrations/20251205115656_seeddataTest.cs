using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaktabShop.Infra.SqlServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class seeddataTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DeletedAt", "Description", "ImageUrl", "Name", "Price", "Stock", "UptatedAt" },
                values: new object[] { 7, 2, null, "«کت سفید با بافت خز ـ انتخابی لوکس و خاص برای فصل سرما. این کت جلوه‌ی زمستانی دارد و می‌تواند نقطه تمرکز استایل شما باشد؛ مناسب برای مهمانی، بیرون رفتن یا خیابان‌گردی در هوای سرد.»", "/images/products/women-test.jpg", "آیتم تست", 1000m, 100, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
