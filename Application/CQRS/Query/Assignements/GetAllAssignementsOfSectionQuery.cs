using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetAllAssignementsOfSectionQuery : IQuery<IEnumerable<Assignment>>
    {
        public int SectionId {  get; set; } 
        public string ProfOrInstUserName { get; set; }    
        public bool IsInstructor {  get; set; } 
    }
}
