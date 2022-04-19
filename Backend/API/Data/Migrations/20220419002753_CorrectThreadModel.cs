using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class CorrectThreadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserThread_Users_UserId",
                table: "UserThread");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserThread",
                table: "UserThread");

            migrationBuilder.RenameTable(
                name: "UserThread",
                newName: "UserThreads");

            migrationBuilder.RenameIndex(
                name: "IX_UserThread_UserId",
                table: "UserThreads",
                newName: "IX_UserThreads_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserThreads",
                table: "UserThreads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreads_Users_UserId",
                table: "UserThreads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserThreads_Users_UserId",
                table: "UserThreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserThreads",
                table: "UserThreads");

            migrationBuilder.RenameTable(
                name: "UserThreads",
                newName: "UserThread");

            migrationBuilder.RenameIndex(
                name: "IX_UserThreads_UserId",
                table: "UserThread",
                newName: "IX_UserThread_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserThread",
                table: "UserThread",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserThread_Users_UserId",
                table: "UserThread",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
