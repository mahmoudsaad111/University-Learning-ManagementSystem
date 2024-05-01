using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateTypeOfDeadLineOfExamToTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamPlace_CourseCycles_CourseCycleId",
                table: "ExamPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamPlace_Courses_CourseId",
                table: "ExamPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamPlace_Sections_SectionId",
                table: "ExamPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamPlace_ExamPlaceId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceQuestion_Exams_ExamId",
                table: "MultipleChoiceQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInMCQ_MultipleChoiceQuestion_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQ");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInMCQ_StudentExam_StudentExamId",
                table: "StudentAnswerInMCQ");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInTFQ_StudentExam_StudentExamId",
                table: "StudentAnswerInTFQ");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInTFQ_TrueFalseQuestion_TrueFalseQuestionId",
                table: "StudentAnswerInTFQ");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Exams_ExamId",
                table: "StudentExam");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Students_StudentId",
                table: "StudentExam");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueFalseQuestion_Exams_ExamId",
                table: "TrueFalseQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrueFalseQuestion",
                table: "TrueFalseQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAnswerInTFQ",
                table: "StudentAnswerInTFQ");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAnswerInMCQ",
                table: "StudentAnswerInMCQ");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceQuestion",
                table: "MultipleChoiceQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamPlace",
                table: "ExamPlace");

            migrationBuilder.RenameTable(
                name: "TrueFalseQuestion",
                newName: "TrueFalseQuestions");

            migrationBuilder.RenameTable(
                name: "StudentExam",
                newName: "StudentExams");

            migrationBuilder.RenameTable(
                name: "StudentAnswerInTFQ",
                newName: "StudentAnswerInTFQs");

            migrationBuilder.RenameTable(
                name: "StudentAnswerInMCQ",
                newName: "StudentAnswerInMCQs");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceQuestion",
                newName: "MultipleChoiceQuestions");

            migrationBuilder.RenameTable(
                name: "ExamPlace",
                newName: "ExamPlaces");

            migrationBuilder.RenameIndex(
                name: "IX_TrueFalseQuestion_ExamId",
                table: "TrueFalseQuestions",
                newName: "IX_TrueFalseQuestions_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExam_StudentId_ExamId",
                table: "StudentExams",
                newName: "IX_StudentExams_StudentId_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExam_ExamId",
                table: "StudentExams",
                newName: "IX_StudentExams_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInTFQ_TrueFalseQuestionId",
                table: "StudentAnswerInTFQs",
                newName: "IX_StudentAnswerInTFQs_TrueFalseQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInTFQ_StudentExamId_TrueFalseQuestionId",
                table: "StudentAnswerInTFQs",
                newName: "IX_StudentAnswerInTFQs_StudentExamId_TrueFalseQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInMCQ_StudentExamId_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQs",
                newName: "IX_StudentAnswerInMCQs_StudentExamId_MultipleChoiceQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInMCQ_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQs",
                newName: "IX_StudentAnswerInMCQs_MultipleChoiceQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceQuestion_ExamId",
                table: "MultipleChoiceQuestions",
                newName: "IX_MultipleChoiceQuestions_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamPlace_SectionId",
                table: "ExamPlaces",
                newName: "IX_ExamPlaces_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamPlace_CourseId",
                table: "ExamPlaces",
                newName: "IX_ExamPlaces_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamPlace_CourseCycleId",
                table: "ExamPlaces",
                newName: "IX_ExamPlaces_CourseCycleId");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DeadLine",
                table: "Exams",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrueFalseQuestions",
                table: "TrueFalseQuestions",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExams",
                table: "StudentExams",
                column: "StudentExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAnswerInTFQs",
                table: "StudentAnswerInTFQs",
                column: "StudentAnswersInTFQId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAnswerInMCQs",
                table: "StudentAnswerInMCQs",
                column: "StudentAnswerInMCQId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceQuestions",
                table: "MultipleChoiceQuestions",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamPlaces",
                table: "ExamPlaces",
                column: "ExamPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPlaces_CourseCycles_CourseCycleId",
                table: "ExamPlaces",
                column: "CourseCycleId",
                principalTable: "CourseCycles",
                principalColumn: "CourseCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPlaces_Courses_CourseId",
                table: "ExamPlaces",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPlaces_Sections_SectionId",
                table: "ExamPlaces",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamPlaces_ExamPlaceId",
                table: "Exams",
                column: "ExamPlaceId",
                principalTable: "ExamPlaces",
                principalColumn: "ExamPlaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceQuestions_Exams_ExamId",
                table: "MultipleChoiceQuestions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInMCQs_MultipleChoiceQuestions_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQs",
                column: "MultipleChoiceQuestionId",
                principalTable: "MultipleChoiceQuestions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInMCQs_StudentExams_StudentExamId",
                table: "StudentAnswerInMCQs",
                column: "StudentExamId",
                principalTable: "StudentExams",
                principalColumn: "StudentExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInTFQs_StudentExams_StudentExamId",
                table: "StudentAnswerInTFQs",
                column: "StudentExamId",
                principalTable: "StudentExams",
                principalColumn: "StudentExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInTFQs_TrueFalseQuestions_TrueFalseQuestionId",
                table: "StudentAnswerInTFQs",
                column: "TrueFalseQuestionId",
                principalTable: "TrueFalseQuestions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExams_Exams_ExamId",
                table: "StudentExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExams_Students_StudentId",
                table: "StudentExams",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueFalseQuestions_Exams_ExamId",
                table: "TrueFalseQuestions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamPlaces_CourseCycles_CourseCycleId",
                table: "ExamPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamPlaces_Courses_CourseId",
                table: "ExamPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamPlaces_Sections_SectionId",
                table: "ExamPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamPlaces_ExamPlaceId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceQuestions_Exams_ExamId",
                table: "MultipleChoiceQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInMCQs_MultipleChoiceQuestions_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInMCQs_StudentExams_StudentExamId",
                table: "StudentAnswerInMCQs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInTFQs_StudentExams_StudentExamId",
                table: "StudentAnswerInTFQs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswerInTFQs_TrueFalseQuestions_TrueFalseQuestionId",
                table: "StudentAnswerInTFQs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExams_Exams_ExamId",
                table: "StudentExams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExams_Students_StudentId",
                table: "StudentExams");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueFalseQuestions_Exams_ExamId",
                table: "TrueFalseQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrueFalseQuestions",
                table: "TrueFalseQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExams",
                table: "StudentExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAnswerInTFQs",
                table: "StudentAnswerInTFQs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAnswerInMCQs",
                table: "StudentAnswerInMCQs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceQuestions",
                table: "MultipleChoiceQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamPlaces",
                table: "ExamPlaces");

            migrationBuilder.RenameTable(
                name: "TrueFalseQuestions",
                newName: "TrueFalseQuestion");

            migrationBuilder.RenameTable(
                name: "StudentExams",
                newName: "StudentExam");

            migrationBuilder.RenameTable(
                name: "StudentAnswerInTFQs",
                newName: "StudentAnswerInTFQ");

            migrationBuilder.RenameTable(
                name: "StudentAnswerInMCQs",
                newName: "StudentAnswerInMCQ");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceQuestions",
                newName: "MultipleChoiceQuestion");

            migrationBuilder.RenameTable(
                name: "ExamPlaces",
                newName: "ExamPlace");

            migrationBuilder.RenameIndex(
                name: "IX_TrueFalseQuestions_ExamId",
                table: "TrueFalseQuestion",
                newName: "IX_TrueFalseQuestion_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExams_StudentId_ExamId",
                table: "StudentExam",
                newName: "IX_StudentExam_StudentId_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExams_ExamId",
                table: "StudentExam",
                newName: "IX_StudentExam_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInTFQs_TrueFalseQuestionId",
                table: "StudentAnswerInTFQ",
                newName: "IX_StudentAnswerInTFQ_TrueFalseQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInTFQs_StudentExamId_TrueFalseQuestionId",
                table: "StudentAnswerInTFQ",
                newName: "IX_StudentAnswerInTFQ_StudentExamId_TrueFalseQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInMCQs_StudentExamId_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQ",
                newName: "IX_StudentAnswerInMCQ_StudentExamId_MultipleChoiceQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswerInMCQs_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQ",
                newName: "IX_StudentAnswerInMCQ_MultipleChoiceQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceQuestions_ExamId",
                table: "MultipleChoiceQuestion",
                newName: "IX_MultipleChoiceQuestion_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamPlaces_SectionId",
                table: "ExamPlace",
                newName: "IX_ExamPlace_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamPlaces_CourseId",
                table: "ExamPlace",
                newName: "IX_ExamPlace_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamPlaces_CourseCycleId",
                table: "ExamPlace",
                newName: "IX_ExamPlace_CourseCycleId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeadLine",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrueFalseQuestion",
                table: "TrueFalseQuestion",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam",
                column: "StudentExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAnswerInTFQ",
                table: "StudentAnswerInTFQ",
                column: "StudentAnswersInTFQId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAnswerInMCQ",
                table: "StudentAnswerInMCQ",
                column: "StudentAnswerInMCQId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceQuestion",
                table: "MultipleChoiceQuestion",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamPlace",
                table: "ExamPlace",
                column: "ExamPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPlace_CourseCycles_CourseCycleId",
                table: "ExamPlace",
                column: "CourseCycleId",
                principalTable: "CourseCycles",
                principalColumn: "CourseCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPlace_Courses_CourseId",
                table: "ExamPlace",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPlace_Sections_SectionId",
                table: "ExamPlace",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamPlace_ExamPlaceId",
                table: "Exams",
                column: "ExamPlaceId",
                principalTable: "ExamPlace",
                principalColumn: "ExamPlaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceQuestion_Exams_ExamId",
                table: "MultipleChoiceQuestion",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInMCQ_MultipleChoiceQuestion_MultipleChoiceQuestionId",
                table: "StudentAnswerInMCQ",
                column: "MultipleChoiceQuestionId",
                principalTable: "MultipleChoiceQuestion",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInMCQ_StudentExam_StudentExamId",
                table: "StudentAnswerInMCQ",
                column: "StudentExamId",
                principalTable: "StudentExam",
                principalColumn: "StudentExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInTFQ_StudentExam_StudentExamId",
                table: "StudentAnswerInTFQ",
                column: "StudentExamId",
                principalTable: "StudentExam",
                principalColumn: "StudentExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswerInTFQ_TrueFalseQuestion_TrueFalseQuestionId",
                table: "StudentAnswerInTFQ",
                column: "TrueFalseQuestionId",
                principalTable: "TrueFalseQuestion",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Exams_ExamId",
                table: "StudentExam",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Students_StudentId",
                table: "StudentExam",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueFalseQuestion_Exams_ExamId",
                table: "TrueFalseQuestion",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
