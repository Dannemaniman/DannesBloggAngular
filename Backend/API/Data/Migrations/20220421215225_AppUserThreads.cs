using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AppUserThreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserThreads_UserThreads_ThreadId",
                table: "UserThreads");

            migrationBuilder.DropIndex(
                name: "IX_UserThreads_ThreadId",
                table: "UserThreads");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "UserThreads");

            migrationBuilder.RenameColumn(
                name: "Views",
                table: "UserThreads",
                newName: "ViewsCount");

            migrationBuilder.RenameColumn(
                name: "Replies",
                table: "UserThreads",
                newName: "RepliesCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ViewsCount",
                table: "UserThreads",
                newName: "Views");

            migrationBuilder.RenameColumn(
                name: "RepliesCount",
                table: "UserThreads",
                newName: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "UserThreads",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserThreads_ThreadId",
                table: "UserThreads",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreads_UserThreads_ThreadId",
                table: "UserThreads",
                column: "ThreadId",
                principalTable: "UserThreads",
                principalColumn: "Id");
        }
    }
}
