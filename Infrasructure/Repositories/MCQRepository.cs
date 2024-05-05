using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.MCQs;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MCQRepository : BaseRepository<MultipleChoiceQuestion>, IMCQRepository
    {
        public MCQRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<MCQTextOPtionsAnswerDto>> GetAllMCQDetailsOfExams(int ExamId)
        {
            var MCQDetails = await (from mcq in _appDbContext.MultipleChoiceQuestions
                                    where mcq.ExamId == ExamId
                                    select new MCQTextOPtionsAnswerDto
                                    {
                                        QuestionId = mcq.QuestionId,
                                        Text = mcq.Text,
                                        OptionA = mcq.OptionA,
                                        OptionB = mcq.OptionB,
                                        OptionC = mcq.OptionC,
                                        OptionD = mcq.OptionD,
                                        CorrectAnswer = mcq.CorrectAnswer,
                                        Degree = mcq.Degree
                                    }
                                  ).ToListAsync();

            return MCQDetails;
        }

        public async Task<IEnumerable<MCQCorrectAnswer>> GetMCQOfExamWithCorrectAnswers(int ExamId)
        {
            var MCQCorrectAnswer = await (from mcq in _appDbContext.MultipleChoiceQuestions
                                          where mcq.ExamId == ExamId
                                          select new MCQCorrectAnswer
                                          {
                                              ExamId = ExamId,
                                              MCQCorrectOption = mcq.CorrectAnswer,
                                              Degree= mcq.Degree,
                                              QuestionId = mcq.QuestionId
                                          }).ToListAsync();

            return MCQCorrectAnswer;
        }

    }
}
