using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechCommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuantityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "037d503b-6373-4f36-b9c5-aa95f5fe04f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92876154-ab07-4c8d-ac6e-85a34e72e8ca");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38938bc3-8a2d-443e-bfa3-b8344cd05b57", null, "Admin", "admin" },
                    { "a42a2fa4-1127-4c15-b50a-f8a29aa78e87", null, "Client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38938bc3-8a2d-443e-bfa3-b8344cd05b57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42a2fa4-1127-4c15-b50a-f8a29aa78e87");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartProducts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "037d503b-6373-4f36-b9c5-aa95f5fe04f6", null, "Admin", "admin" },
                    { "92876154-ab07-4c8d-ac6e-85a34e72e8ca", null, "Client", "client" }
                });
        }
    }
}
