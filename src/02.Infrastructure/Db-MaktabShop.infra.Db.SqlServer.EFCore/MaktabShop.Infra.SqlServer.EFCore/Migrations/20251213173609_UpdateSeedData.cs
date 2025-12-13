using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaktabShop.Infra.SqlServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: "NormaUser");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Role",
                value: "NormaUser");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Role",
                value: "NormaUser");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DeletedAt", "FirstName", "LastName", "PasswordHash", "Phone", "Role", "UptatedAt", "Username", "Wallet" },
                values: new object[] { 4, "تهران", null, "ادمین", "1", "123456", "09111223342", "Admin", null, "admin", 100000000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
