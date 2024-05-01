using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ModifyCreatedAtFromIntToDateTimeWithNameSubmitedAtOnStudentExamEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentExams");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "StudentAnswerInTFQs",
                newName: "StudentAnswer");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitedAt",
                table: "StudentExams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitedAt",
                table: "StudentExams");

            migrationBuilder.RenameColumn(
                name: "StudentAnswer",
                table: "StudentAnswerInTFQs",
                newName: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "CreatedAt",
                table: "StudentExams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
