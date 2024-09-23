using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVProject.Migrations
{
    /// <inheritdoc />
    public partial class RoleView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21311866-76ac-4462-a6e1-ebdd2cc356df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ba44d79-baff-486d-8e97-964dcc1d17e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75d73916-be7a-4747-aa9d-ccac7c2b27a8", "603ef368-1391-4502-a9be-4dfbdcaf8e4d", "Admin", "admin" },
                    { "da6560ab-3f27-4ccb-8393-f37b8f316a20", "ea703256-4691-4e6b-b030-3017a058eeb1", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75d73916-be7a-4747-aa9d-ccac7c2b27a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da6560ab-3f27-4ccb-8393-f37b8f316a20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21311866-76ac-4462-a6e1-ebdd2cc356df", "1046c8b4-88fa-44e9-979f-6d493b8c39c8", "User", "user" },
                    { "7ba44d79-baff-486d-8e97-964dcc1d17e6", "9813e119-cd6d-4d0b-b983-471657648e26", "Admin", "admin" }
                });
        }
    }
}
