using Application.Common.Interfaces.CQRSInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.TFQs
{
    public class DeleteTFQCommand :ICommand<int>
    {
        public int TFQId { get; set; }  
    }
}
