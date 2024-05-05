using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Contract.Dto.QuestionsOfExam;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IExamRepository : IBaseRepository<Exam>
    {

        public Task<Tuple<bool, ExamPlace>> CheckIfExamDataValidHasValidExamPlace(ExamDto ExamDto);
        public Task<IEnumerable<QuizsToSectionOfInstructorDto>> GetAllQuizsToSectionOfInstructors(int sectionId );
        public Task<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>> GetAllExamsOfCourseCycleOfProfessor(int courseCycleId);
        public Task<bool> UpdateExamFullMarksAccordingToCommandOfQuestion(int ExamId, int NewQuestionDegree, int OldQuestionDegree = 0);
        public Task<Exam> GetExamIncludinigExamPlaceByExamId(int ExamId);
        //  public Task<bool> CheckIfStudentHasAccessToExam(int ExamId, string StudentUserName); 

    //    public Task<bool> CheckIfInstrucotrOrProfessorHasAccessToExam(int ExamId, string CreatorExamUserName);
        public Task<ExamWrokNowDto> GetExamWorkNow (int ExamId);

        public Task<IEnumerable<ExamOfStudentToDto>> GetAllExamsOfStudentInCourseCycle(int courseCylceId, int studentId);
        public Task<IEnumerable<ExamOfStudentToDto>> GetAllExamsOfStudentInSection(int sectionId , int studentId);
    }
}
