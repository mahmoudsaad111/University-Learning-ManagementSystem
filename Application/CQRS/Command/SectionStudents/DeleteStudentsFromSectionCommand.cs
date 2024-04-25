using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.SectionStudents
{
    public class DeleteStudentsFromSectionCommand : ICommand<IEnumerable<string>>
    { 
       public IEnumerable<string> StudentsUserNames { get; set; }
       public int SectionId { get; set; }
    }
}
