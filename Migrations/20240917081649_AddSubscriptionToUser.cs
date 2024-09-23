using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Movies_MovieId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_MovieId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39deb3d0-5b68-427e-9416-69673b716505");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "752735a4-2ae9-4162-a86b-b6148acc2976");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89a0a1bb-1b01-4252-b632-8c3b74c6e75c", "36bc24f7-764a-43e4-b0ef-b4eea6a2bf52", "Admin", "admin" },
                    { "ccf636ca-c557-43e2-8ff7-efce0ea75026", "90406c70-fc2d-4500-ab5e-c725ad84fc88", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89a0a1bb-1b01-4252-b632-8c3b74c6e75c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccf636ca-c557-43e2-8ff7-efce0ea75026");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpiration",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39deb3d0-5b68-427e-9416-69673b716505", "fe9329a3-cd0f-4e0e-8c04-22f37553ceb1", "Admin", "admin" },
                    { "752735a4-2ae9-4162-a86b-b6148acc2976", "461ad7a2-c162-4b00-9bec-ad6b354a0ea9", "User", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_MovieId",
                table: "Carts",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Movies_MovieId",
                table: "Carts",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
