using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechCommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRate_Products_ProductId",
                table: "ProductRate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRate",
                table: "ProductRate");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0b18300-774f-49d7-8790-b4e071041cbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df6a7b02-3e96-40db-8884-665d22aa456a");

            migrationBuilder.RenameTable(
                name: "ProductRate",
                newName: "Rates");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRate_ProductId",
                table: "Rates",
                newName: "IX_Rates_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                table: "Rates",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a6860fe-70d6-4a72-9717-19b7be2ec09a", null, "Admin", "admin" },
                    { "6a1e0597-b6dc-47b8-8462-e66ed42cfab4", null, "Client", "client" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Products_ProductId",
                table: "Rates",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Products_ProductId",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                table: "Rates");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a6860fe-70d6-4a72-9717-19b7be2ec09a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a1e0597-b6dc-47b8-8462-e66ed42cfab4");

            migrationBuilder.RenameTable(
                name: "Rates",
                newName: "ProductRate");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_ProductId",
                table: "ProductRate",
                newName: "IX_ProductRate_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRate",
                table: "ProductRate",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d0b18300-774f-49d7-8790-b4e071041cbd", null, "Client", "client" },
                    { "df6a7b02-3e96-40db-8884-665d22aa456a", null, "Admin", "admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRate_Products_ProductId",
                table: "ProductRate",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
