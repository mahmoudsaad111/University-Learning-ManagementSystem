using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.Exams;
using Contract.Dto.MCQs;
using Contract.Dto.StudentExamDto;
using Contract.Dto.TFQs;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentExamRepository : BaseRepository<StudentExam>, IStudentExamRepository
    {
        private readonly IStudentAnswerInMCQRepository _studentAnswerInMCQRepository;
        private readonly IStudentAnswerInTFQRepository _studentAnswerInTFQRepository;
        private readonly IMCQRepository _MCQRepository;
        private readonly ITFQRepository _TFQRepository;

        public StudentExamRepository(AppDbContext _appDbContext, IStudentAnswerInMCQRepository studentAnswerInMCQRepository, IStudentAnswerInTFQRepository studentAnswerInTFQRepository, IMCQRepository mCQRepository, ITFQRepository tFQRepository) : base(_appDbContext)
        {
            this._studentAnswerInMCQRepository = studentAnswerInMCQRepository;
            this._studentAnswerInTFQRepository = studentAnswerInTFQRepository;
            this._MCQRepository = mCQRepository;
            this._TFQRepository = tFQRepository;
        }


        public async Task<Tuple<bool, int>> AddStudentMCQAnswerOfExam(int studentExamId, List<MCQCorrectAnswer> mCQCorrectAnswers, List<MCQStudentSubmitionDto> MCQStudentAnswers)
        {
            mCQCorrectAnswers.Sort((mcq1, mcq2) => mcq1.QuestionId.CompareTo(mcq2.QuestionId));

            MCQStudentAnswers.Sort((mcq1, mcq2) => mcq1.QuestionId.CompareTo(mcq2.QuestionId));

            int SumOfCorrectAnswersDegrees = 0;
            for (int i = 0; i < mCQCorrectAnswers.Count; i++)
            {
                if (MCQStudentAnswers[i].QuestionId == mCQCorrectAnswers[i].QuestionId)
                {
                    if (MCQStudentAnswers[i].StudentAnswer == mCQCorrectAnswers[i].MCQCorrectOption)
                        SumOfCorrectAnswersDegrees += mCQCorrectAnswers[i].Degree;

                    var addedNow = _studentAnswerInMCQRepository.Create(new StudentAnswerInMCQ
                    {
                        MultipleChoiceQuestionId = mCQCorrectAnswers[i].QuestionId,
                        OptionSelectedByStudent = MCQStudentAnswers[i].StudentAnswer,
                        StudentExamId = studentExamId
                    });

                    if (addedNow is null)
                        return new Tuple<bool, int>(false, 0);

                }
            }

            return new Tuple<bool, int>(true, SumOfCorrectAnswersDegrees);
        }

        public async Task<Tuple<bool, int>> AddStudentTFQAnswerOfExam(int studentExamId, List<TFQCorrectAnswer> tFQCorrectAnswers, List<TFQStudentSubmitionDto> TFQStudentAnswers)
        {
            tFQCorrectAnswers.Sort((tfq1, tfq2) => tfq1.QuestionId.CompareTo(tfq2.QuestionId));
            TFQStudentAnswers.Sort((tfq1, tfq2) => tfq1.QuestionId.CompareTo(tfq2.QuestionId));

            int SumOfCorrectAnswersDegrees = 0;
            for (int i = 0; i < tFQCorrectAnswers.Count; i++)
            {
                if (TFQStudentAnswers[i].QuestionId == tFQCorrectAnswers[i].QuestionId)
                {
                    if (TFQStudentAnswers[i].IsTrue == tFQCorrectAnswers[i].IsTrue)
                        SumOfCorrectAnswersDegrees += tFQCorrectAnswers[i].Degree;

                    var addedNow = _studentAnswerInTFQRepository.Create(new StudentAnswerInTFQ
                    {
                        StudentExamId = studentExamId,
                        TrueFalseQuestionId = tFQCorrectAnswers[i].QuestionId,
                        StudentAnswer = TFQStudentAnswers[i].IsTrue

                    });

                    if (addedNow is null)
                        return new Tuple<bool, int>(false, 0);
                }
            }

            return new Tuple<bool, int>(true, SumOfCorrectAnswersDegrees);
        }

        public async Task<StudentAnswersOfExamDto> GetStudentsAnswerOfExamWithModelAnswer(string studentUserName, int ExamId)
        {
            var StudentAnswersOfExam = await (from user in _appDbContext.Users
                                              where user.UserName == studentUserName
                                              from exam in _appDbContext.Exams
                                              where exam.ExamId == ExamId
                                              from studentExam in _appDbContext.StudentExams
                                              where user.Id == studentExam.StudentId && exam.ExamId == studentExam.ExamId

                                              select new StudentAnswersOfExamDto
                                              {
                                                  StudentId = user.Id,
                                                  StudentFullName = user.FullName,
                                                  StudentTotalMarks = studentExam.MarkOfStudentInExam,
                                                  StudentUserName = user.UserName,
                                                  StudentImageUrl = user.ImageUrl,
                                                  StudentSubmissionDate = studentExam.SubmitedAt,
                                                  MCQInfoWithStudentAnswerDtos = (from mcq in _appDbContext.MultipleChoiceQuestions
                                                                                  where mcq.ExamId == exam.ExamId
                                                                                  from studentMcqAns in _appDbContext.StudentAnswerInMCQs
                                                                                  where studentMcqAns.StudentExamId == studentExam.StudentExamId && studentMcqAns.MultipleChoiceQuestionId == mcq.QuestionId
                                                                                  select new MCQInfoWithStudentAnswerDto
                                                                                  {
                                                                                      Text = mcq.Text,
                                                                                      OptionA = mcq.OptionA,
                                                                                      OptionB = mcq.OptionB,
                                                                                      OptionC = mcq.OptionC,
                                                                                      OptionD = mcq.OptionD,
                                                                                      Degree = mcq.Degree,
                                                                                      CorrectAnswer = mcq.CorrectAnswer,
                                                                                      StudentAnswer = studentMcqAns.OptionSelectedByStudent
                                                                                  }).ToList(),

                                                  TFQInfoWithStudentAnswerDtos = (
                                                                                  from tfq in _appDbContext.TrueFalseQuestions
                                                                                  where tfq.ExamId == ExamId
                                                                                  from studentTfqAns in _appDbContext.StudentAnswerInTFQs
                                                                                  where studentTfqAns.StudentExamId == studentExam.StudentExamId && studentTfqAns.TrueFalseQuestionId == tfq.QuestionId
                                                                                  select new TFQInfoWithStudentAnswerDto
                                                                                  {
                                                                                      Text = tfq.Text,
                                                                                      Degree = tfq.Degree,
                                                                                      CorectAnswer = tfq.IsTrue,
                                                                                      StudetAnswer = studentTfqAns.StudentAnswer
                                                                                  }).ToList()

                                              }).ToListAsync();

            return StudentAnswersOfExam.FirstOrDefault();
        }
    }
}
