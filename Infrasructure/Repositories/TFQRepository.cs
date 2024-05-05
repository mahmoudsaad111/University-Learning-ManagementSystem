using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
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
    public class TFQRepository : BaseRepository<TrueFalseQuestion>, ITFQRepository
    {
        public TFQRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<TFQTextAsnwerDto>> GetAllTFQDetailsOfExams(int ExamId)
        {
            var TFQDetails = await (from tfq in _appDbContext.TrueFalseQuestions
                                    where tfq.ExamId == ExamId
                                    select new TFQTextAsnwerDto
                                    {
                                        QuestionId = tfq.QuestionId,
                                        Text = tfq.Text,
                                        IsTrue = tfq.IsTrue,
                                        Degree = tfq.Degree,
                                    }
                                   ).ToListAsync();
            return TFQDetails;
        }

        public async Task<IEnumerable<TFQCorrectAnswer>> GetMTFOfExamWithCorrectAnswer(int ExamId)
        {
            var TFQCorrectAnswer = await (from tfq in _appDbContext.TrueFalseQuestions
                                          where tfq.ExamId == ExamId
                                          select new TFQCorrectAnswer
                                          {
                                              ExamId = ExamId,
                                              IsTrue = tfq.IsTrue,
                                              Degree=tfq.Degree,
                                              QuestionId = tfq.QuestionId
                                          }).ToListAsync();
            return TFQCorrectAnswer;
        }
    }
}
