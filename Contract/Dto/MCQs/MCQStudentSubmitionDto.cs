using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.MCQs
{
    public class MCQStudentSubmitionDto
    {
        public int QuestionId { get; set; }
        public MCQOptions StudentAnswer { get; set; }
    }
}
