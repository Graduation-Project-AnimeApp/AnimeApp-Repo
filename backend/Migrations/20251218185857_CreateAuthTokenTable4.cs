using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFlixBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateAuthTokenTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Tokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Tokens",
                type: "datetime2",
                nullable: true);
        }
    }
}
