using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class updatedReturnModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlockList_AspNetUsers_UserId",
                table: "UserBlockList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReplies_AspNetUsers_UserId",
                table: "UserReplies");

            migrationBuilder.DropIndex(
                name: "IX_UserBlockList_UserId",
                table: "UserBlockList");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserReplies",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReplies_UserId",
                table: "UserReplies",
                newName: "IX_UserReplies_AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserReplies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserReplies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Duration",
                table: "UserBlockList",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReplies_AspNetUsers_AppUserId",
                table: "UserReplies",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReplies_AspNetUsers_AppUserId",
                table: "UserReplies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserReplies");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserReplies");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserReplies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReplies_AppUserId",
                table: "UserReplies",
                newName: "IX_UserReplies_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Duration",
                table: "UserBlockList",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBlockList_UserId",
                table: "UserBlockList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlockList_AspNetUsers_UserId",
                table: "UserBlockList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReplies_AspNetUsers_UserId",
                table: "UserReplies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
