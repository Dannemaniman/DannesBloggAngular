using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class UserRepliesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReply_Users_UserId",
                table: "UserReply");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReply_UserThreads_ThreadId",
                table: "UserReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReply",
                table: "UserReply");

            migrationBuilder.RenameTable(
                name: "UserReply",
                newName: "UserReplies");

            migrationBuilder.RenameIndex(
                name: "IX_UserReply_UserId",
                table: "UserReplies",
                newName: "IX_UserReplies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReply_ThreadId",
                table: "UserReplies",
                newName: "IX_UserReplies_ThreadId");

            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "UserThreads",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReplies",
                table: "UserReplies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserThreads_ThreadId",
                table: "UserThreads",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReplies_Users_UserId",
                table: "UserReplies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReplies_UserThreads_ThreadId",
                table: "UserReplies",
                column: "ThreadId",
                principalTable: "UserThreads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserThreads_UserThreads_ThreadId",
                table: "UserThreads",
                column: "ThreadId",
                principalTable: "UserThreads",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReplies_Users_UserId",
                table: "UserReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReplies_UserThreads_ThreadId",
                table: "UserReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserThreads_UserThreads_ThreadId",
                table: "UserThreads");

            migrationBuilder.DropIndex(
                name: "IX_UserThreads_ThreadId",
                table: "UserThreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReplies",
                table: "UserReplies");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "UserThreads");

            migrationBuilder.RenameTable(
                name: "UserReplies",
                newName: "UserReply");

            migrationBuilder.RenameIndex(
                name: "IX_UserReplies_UserId",
                table: "UserReply",
                newName: "IX_UserReply_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReplies_ThreadId",
                table: "UserReply",
                newName: "IX_UserReply_ThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReply",
                table: "UserReply",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReply_Users_UserId",
                table: "UserReply",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReply_UserThreads_ThreadId",
                table: "UserReply",
                column: "ThreadId",
                principalTable: "UserThreads",
                principalColumn: "Id");
        }
    }
}
