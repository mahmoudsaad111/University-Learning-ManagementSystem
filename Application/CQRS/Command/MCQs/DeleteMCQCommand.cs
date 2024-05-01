using Application.Common.Interfaces.CQRSInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.MCQs
{
    public class DeleteMCQCommand :ICommand<int>
    {
        public int MCQId { get; set; }  
    }
}
