using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.MCQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.MCQs
{
    public class UpdateMCQCommand :ICommand<MultipleChoiceQuestion>
    {
        public int MCQId {  get; set; }    
        public MCQDto MCQDto { get; set; }
    }
}
