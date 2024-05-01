using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.TFQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.TFQs
{
    public class CreateTFQCommand : ICommand<TrueFalseQuestion>
    {
        public TFQDto TFQDto { get; set; }
    }
}
