using Application.Common.Interfaces.Presistance;
using Contract.Dto.MCQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IMCQRepository : IBaseRepository<MultipleChoiceQuestion>
    {
        public Task<IEnumerable<MCQCorrectAnswer>> GetMCQOfExamWithCorrectAnswers(int ExamId);
        public Task<IEnumerable<MCQTextOPtionsAnswerDto>> GetAllMCQDetailsOfExams(int ExamId);
    }
}
