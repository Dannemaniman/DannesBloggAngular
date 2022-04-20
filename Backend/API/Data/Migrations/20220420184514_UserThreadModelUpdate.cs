using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class UserThreadModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "UserThreads",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "UserThreads",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UserThreads");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "UserThreads",
                newName: "Category");
        }
    }
}
