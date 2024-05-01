using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Contract.Dto.MCQs;
using Contract.Dto.StudentExamDto;
using Contract.Dto.TFQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IStudentExamRepository : IBaseRepository<StudentExam> 
    {
        public Task<Tuple<bool, int>> AddStudentMCQAnswerOfExam(int studentExamId, List<MCQCorrectAnswer> mCQCorrectAnswers, List<MCQStudentSubmitionDto> MCQStudentAnswers);
 
        public Task<Tuple<bool, int>> AddStudentTFQAnswerOfExam(int studentExamId, List<TFQCorrectAnswer> tFQCorrectAnswers , List<TFQStudentSubmitionDto> TFQStudentAnswers);

        public Task<StudentAnswersOfExamDto> GetStudentsAnswerOfExamWithModelAnswer(string studentUserName, int ExamId); 
    }
}
