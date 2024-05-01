using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.MCQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.MCQs
{
    public class CreateMCQCommand : ICommand<MultipleChoiceQuestion>
    {
        public MCQDto MCQDto { get; set; }
    }
}
