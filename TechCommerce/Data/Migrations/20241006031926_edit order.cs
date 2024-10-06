using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechCommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class editorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "059b2ed6-2620-440c-9957-e0631cb7d9c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ccdc597-3dc6-4551-8092-5982f2c3b5f6");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94d2708f-8dca-4c48-915d-0702a375d7d2", null, "Client", "client" },
                    { "e2adfc17-8685-4726-a843-71d3c1739b9d", null, "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94d2708f-8dca-4c48-915d-0702a375d7d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2adfc17-8685-4726-a843-71d3c1739b9d");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "059b2ed6-2620-440c-9957-e0631cb7d9c6", null, "Client", "client" },
                    { "5ccdc597-3dc6-4551-8092-5982f2c3b5f6", null, "Admin", "admin" }
                });
        }
    }
}
