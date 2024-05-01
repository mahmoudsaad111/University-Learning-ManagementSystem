using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.StudentExamDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.StudentExams
{
    public class SubmitExamToStudentCommand : ICommand<int>
    {
        public StudentSubmitionOfExamDto studentAnswersOfExamDto { get; set; }
    }
}
