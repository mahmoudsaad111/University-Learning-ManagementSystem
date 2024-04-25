using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AuthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentCourseCycleId",
                table: "StudentSections");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentSectionId_SectionId",
                table: "StudentSections");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentCourseCycleId_SectionId",
                table: "StudentSections",
                columns: new[] { "StudentCourseCycleId", "SectionId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_StudentSections_StudentCourseCycleId_SectionId",
                table: "StudentSections");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentCourseCycleId",
                table: "StudentSections",
                column: "StudentCourseCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSections_StudentSectionId_SectionId",
                table: "StudentSections",
                columns: new[] { "StudentSectionId", "SectionId" },
                unique: true);
        }
    }
}
