using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class UpdatedCreatedAgainagainaadwawdawdawdawd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Created",
                table: "UserThreads",
                type: "BLOB",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Created",
                table: "Users",
                type: "BLOB",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldRowVersion: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserThreads",
                type: "TEXT",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "TEXT",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
