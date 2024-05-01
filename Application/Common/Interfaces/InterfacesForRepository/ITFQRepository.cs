using Application.Common.Interfaces.Presistance;
using Contract.Dto.TFQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ITFQRepository : IBaseRepository<TrueFalseQuestion>
    {
        public Task<IEnumerable<TFQCorrectAnswer>> GetMTFOfExamWithCorrectAnswer(int ExamId);
    }
}
