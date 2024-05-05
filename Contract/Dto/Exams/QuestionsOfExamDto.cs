using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class QuestionsOfExamDto
    {
        public IEnumerable<TFQTextAsnwerDto> TFQs { get; set; }
        public IEnumerable<MCQTextOPtionsAnswerDto> MCQs { get; set; }
    }
}
