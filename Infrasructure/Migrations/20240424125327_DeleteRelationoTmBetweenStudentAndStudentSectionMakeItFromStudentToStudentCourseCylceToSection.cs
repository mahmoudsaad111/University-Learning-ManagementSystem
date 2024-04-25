using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class DeleteRelationoTmBetweenStudentAndStudentSectionMakeItFromStudentToStudentCourseCylceToSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSections_Students_StudentId",
                table: "StudentSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentId",
                table: "StudentSections");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentSections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentSections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentId",
                table: "StudentSections",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSections_Students_StudentId",
                table: "StudentSections",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
