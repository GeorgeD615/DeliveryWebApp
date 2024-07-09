using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFavoriteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Favorites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "UserId", "ProductId" });
        }
    }
}
