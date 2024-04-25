using Application.Common.Interfaces.CQRSInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.SectionStudents
{
    public class AddStudensToSectionCommand : ICommand<IEnumerable<string>>   
    {
        public IEnumerable<string> StudentsUserNames {  get; set; } 
        public int SectionId { get; set; }
    }
}
