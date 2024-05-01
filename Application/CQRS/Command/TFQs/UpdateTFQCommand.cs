using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.TFQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.TFQs
{
    public class UpdateTFQCommand :ICommand<TrueFalseQuestion>
    {
        public int TFQId {  get; set; }    
        public TFQDto TFQDto { get; set; }
    }
}
