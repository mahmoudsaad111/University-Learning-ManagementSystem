using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateAssignemntProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Assignments",
                newName: "StartedAt");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DeadLine",
                table: "Assignments",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "Assignments",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeadLine",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
