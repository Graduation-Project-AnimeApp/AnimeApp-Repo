using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFlixBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateAuthTokenTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthToken_Users_UserId",
                table: "AuthToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthToken",
                table: "AuthToken");

            migrationBuilder.RenameTable(
                name: "AuthToken",
                newName: "Tokens");

            migrationBuilder.RenameIndex(
                name: "IX_AuthToken_UserId",
                table: "Tokens",
                newName: "IX_Tokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthToken_TokenName",
                table: "Tokens",
                newName: "IX_Tokens_TokenName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "AuthToken");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_UserId",
                table: "AuthToken",
                newName: "IX_AuthToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_TokenName",
                table: "AuthToken",
                newName: "IX_AuthToken_TokenName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthToken",
                table: "AuthToken",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthToken_Users_UserId",
                table: "AuthToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
