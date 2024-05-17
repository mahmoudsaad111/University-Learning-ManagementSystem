using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Assignements
{
    public class AssignmentOfSectionToAnyUserDto
    {
        public int SectionId { get; set; }
        public string UserName { get; set; }
        public TypesOfUsers TypeOfUser { get; set; }
    }
}
