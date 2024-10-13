using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechCommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdduseridRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca746bfc-e3a7-49c8-b705-9f7112053877");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc61ae70-96a4-4dd6-8536-6eb1fe2b7bd7");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductRate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d0b18300-774f-49d7-8790-b4e071041cbd", null, "Client", "client" },
                    { "df6a7b02-3e96-40db-8884-665d22aa456a", null, "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0b18300-774f-49d7-8790-b4e071041cbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df6a7b02-3e96-40db-8884-665d22aa456a");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductRate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ca746bfc-e3a7-49c8-b705-9f7112053877", null, "Admin", "admin" },
                    { "cc61ae70-96a4-4dd6-8536-6eb1fe2b7bd7", null, "Client", "client" }
                });
        }
    }
}
