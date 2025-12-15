using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnimeFlixBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimeApiId = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    PreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FavoriteGenres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredMood = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.PreferenceId);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchHistories",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimeApiId = table.Column<int>(type: "int", nullable: false),
                    WatchStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WatchedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchHistories", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_WatchHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 15, 16, 38, 55, 149, DateTimeKind.Local).AddTicks(3500), "esraa@example.com", "e3afed0047b08059d0fada10f400c1e5", "Esraa" },
                    { 2, new DateTime(2025, 12, 15, 16, 38, 55, 149, DateTimeKind.Local).AddTicks(3515), "ali@example.com", "e3afed0047b08059d0fada10f400c1e5", "Ali" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "AnimeApiId", "CreatedAt", "ReviewText", "UserId" },
                values: new object[,]
                {
                    { 1, 32, new DateTime(2025, 12, 15, 16, 38, 55, 149, DateTimeKind.Local).AddTicks(4738), "Amazing anime! Great action scenes.", 1 },
                    { 2, 30, new DateTime(2025, 12, 15, 16, 38, 55, 149, DateTimeKind.Local).AddTicks(4749), "Very emotional story. Loved it!", 2 }
                });

            migrationBuilder.InsertData(
                table: "UserPreferences",
                columns: new[] { "PreferenceId", "FavoriteGenres", "PreferredMood", "UserId" },
                values: new object[,]
                {
                    { 1, "Action,Adventure", "Excited", 1 },
                    { 2, "Romance,Drama", "Relaxed", 2 }
                });

            migrationBuilder.InsertData(
                table: "WatchHistories",
                columns: new[] { "HistoryId", "AnimeApiId", "UserId", "WatchStatus", "WatchedAt" },
                values: new object[,]
                {
                    { 1, 32, 1, "Watching", new DateTime(2025, 12, 14, 16, 38, 55, 149, DateTimeKind.Local).AddTicks(4489) },
                    { 2, 30, 2, "Completed", new DateTime(2025, 12, 10, 16, 38, 55, 149, DateTimeKind.Local).AddTicks(4508) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId",
                table: "UserPreferences",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchHistories_UserId",
                table: "WatchHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "WatchHistories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
