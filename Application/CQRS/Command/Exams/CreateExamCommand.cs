using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.Exams
{
    public class CreateExamCommand : ICommand<Exam>
    {
        public ExamDto ExamDto { get; set; }
    }
}
