using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Application.CQRS.Command.Exams
{
    public class UpdateExamCommand  : ICommand<Exam>
    {
        public int ExamId { get; set; } 
        public UpdateExamDto UpdateExamDto { get; set; }
    }
}
