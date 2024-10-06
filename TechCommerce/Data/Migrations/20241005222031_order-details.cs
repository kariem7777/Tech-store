using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechCommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38938bc3-8a2d-443e-bfa3-b8344cd05b57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42a2fa4-1127-4c15-b50a-f8a29aa78e87");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderProduct",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33b81db8-e535-4e7d-aedb-f176a8f0bfdf", null, "Client", "client" },
                    { "e148eb63-b1a6-4a8b-84a4-c7357136dbe4", null, "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33b81db8-e535-4e7d-aedb-f176a8f0bfdf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e148eb63-b1a6-4a8b-84a4-c7357136dbe4");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderProduct");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38938bc3-8a2d-443e-bfa3-b8344cd05b57", null, "Admin", "admin" },
                    { "a42a2fa4-1127-4c15-b50a-f8a29aa78e87", null, "Client", "client" }
                });
        }
    }
}
