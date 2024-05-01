using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddStudentAnswersOfQuestionsOfExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAnswerInMCQ",
                columns: table => new
                {
                    StudentAnswerInMCQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionSelectedByStudent = table.Column<int>(type: "int", nullable: false),
                    StudentExamId = table.Column<int>(type: "int", nullable: false),
                    MultipleChoiceQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswerInMCQ", x => x.StudentAnswerInMCQId);
                    table.ForeignKey(
                        name: "FK_StudentAnswerInMCQ_MultipleChoiceQuestion_MultipleChoiceQuestionId",
                        column: x => x.MultipleChoiceQuestionId,
                        principalTable: "MultipleChoiceQuestion",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswerInMCQ_StudentExam_StudentExamId",
                        column: x => x.StudentExamId,
                        principalTable: "StudentExam",
                        principalColumn: "StudentExamId");
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswerInTFQ",
                columns: table => new
                {
                    StudentAnswersInTFQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentExamId = table.Column<int>(type: "int", nullable: false),
                    TrueFalseQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswerInTFQ", x => x.StudentAnswersInTFQId);
                    table.ForeignKey(
                        name: "FK_StudentAnswerInTFQ_StudentExam_StudentExamId",
                        column: x => x.StudentExamId,
                        principalTable: "StudentExam",
                        principalColumn: "StudentExamId");
                    table.ForeignKey(
                        name: "FK_StudentAnswerInTFQ_TrueFalseQuestion_TrueFalseQuestionId",
                        column: x => x.TrueFalseQuestionId,
                        principalTable: "TrueFalseQuestion",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerInMCQ_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQ",
                column: "MultipleChoiceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerInMCQ_StudentExamId_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQ",
                columns: new[] { "StudentExamId", "MultipleChoiceQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerInTFQ_StudentExamId_TrueFalseQuestionId",
                table: "StudentAnswerInTFQ",
                columns: new[] { "StudentExamId", "TrueFalseQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerInTFQ_TrueFalseQuestionId",
                table: "StudentAnswerInTFQ",
                column: "TrueFalseQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAnswerInMCQ");

            migrationBuilder.DropTable(
                name: "StudentAnswerInTFQ");
        }
    }
}
