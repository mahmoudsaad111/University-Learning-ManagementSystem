using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Application.CQRS.Command.Exams
{
    public class DeleteExamCommand :ICommand<int>
    {
        public int ExamId {  get; set; }
    }
}
