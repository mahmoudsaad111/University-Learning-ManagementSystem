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
        public Task<IEnumerable<QuizsToSectionOfInstructorDto>> GetQuizsToSectionOfInstructors(int sectionId);
        public Task<bool> UpdateExamFullMarksAccordingToCommandOfQuestion(int ExamId, int NewQuestionDegree, int OldQuestionDegree = 0);
        public Task<Exam> GetExamIncludinigExamPlaceByExamId(int ExamId);
      //  public Task<bool> CheckIfStudentHasAccessToExam(int ExamId, string StudentUserName); 
        public Task<ExamWrokNowDto> GetExamWorkNow (int ExamId);    
    }
}
