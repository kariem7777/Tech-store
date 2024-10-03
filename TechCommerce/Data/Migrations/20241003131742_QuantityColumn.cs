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
                keyValue: "379d6c94-f794-4504-a252-0582c7138044");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b57e5da1-a506-4120-b4cd-bae60f2ee853");

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
                    { "80030aaf-03a1-4809-b6e8-6e81709c2031", null, "Admin", "admin" },
                    { "a90966b3-0eff-4a39-9a38-b85d7a0c659b", null, "Client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80030aaf-03a1-4809-b6e8-6e81709c2031");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a90966b3-0eff-4a39-9a38-b85d7a0c659b");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartProducts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "379d6c94-f794-4504-a252-0582c7138044", null, "Admin", "admin" },
                    { "b57e5da1-a506-4120-b4cd-bae60f2ee853", null, "Client", "client" }
                });
        }
    }
}
