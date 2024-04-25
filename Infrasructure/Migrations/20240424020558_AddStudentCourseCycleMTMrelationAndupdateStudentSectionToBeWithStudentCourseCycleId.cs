using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddStudentCourseCycleMTMrelationAndupdateStudentSectionToBeWithStudentCourseCycleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSections_Students_StudentId",
                table: "StudentSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentId_SectionId",
                table: "StudentSections");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StudentCourseCycleId",
                table: "StudentSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentCourseCycle",
                columns: table => new
                {
                    StudentCourseCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseCycleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseCycle", x => x.StudentCourseCycleId);
                    table.ForeignKey(
                        name: "FK_StudentCourseCycle_CourseCycles_CourseCycleId",
                        column: x => x.CourseCycleId,
                        principalTable: "CourseCycles",
                        principalColumn: "CourseCycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseCycle_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentCourseCycleId",
                table: "StudentSections",
                column: "StudentCourseCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentId",
                table: "StudentSections",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentSectionId_SectionId",
                table: "StudentSections",
                columns: new[] { "StudentSectionId", "SectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseCycle_CourseCycleId",
                table: "StudentCourseCycle",
                column: "CourseCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseCycle_StudentId_StudentCourseCycleId",
                table: "StudentCourseCycle",
                columns: new[] { "StudentId", "StudentCourseCycleId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSections_StudentCourseCycle_StudentCourseCycleId",
                table: "StudentSections",
                column: "StudentCourseCycleId",
                principalTable: "StudentCourseCycle",
                principalColumn: "StudentCourseCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSections_Students_StudentId",
                table: "StudentSections",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSections_StudentCourseCycle_StudentCourseCycleId",
                table: "StudentSections");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSections_Students_StudentId",
                table: "StudentSections");

            migrationBuilder.DropTable(
                name: "StudentCourseCycle");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentCourseCycleId",
                table: "StudentSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentId",
                table: "StudentSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentSectionId_SectionId",
                table: "StudentSections");

            migrationBuilder.DropColumn(
                name: "StudentCourseCycleId",
                table: "StudentSections");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentId_SectionId",
                table: "StudentSections",
                columns: new[] { "StudentId", "SectionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSections_Students_StudentId",
                table: "StudentSections",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
