using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AddedUserReplyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WasCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WasModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    ThreadId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReply_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReply_UserThreads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "UserThreads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReply_ThreadId",
                table: "UserReply",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReply_UserId",
                table: "UserReply",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReply");
        }
    }
}
