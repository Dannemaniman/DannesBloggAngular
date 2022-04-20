using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class UpdatedCreatedAgainagainaadwawdawdawdawdawdawdawdawd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "UserThreads",
                newName: "WasModified");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "UserThreads",
                newName: "WasCreated");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Users",
                newName: "WasModified");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Users",
                newName: "WasCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WasModified",
                table: "UserThreads",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "WasCreated",
                table: "UserThreads",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "WasModified",
                table: "Users",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "WasCreated",
                table: "Users",
                newName: "Created");
        }
    }
}
