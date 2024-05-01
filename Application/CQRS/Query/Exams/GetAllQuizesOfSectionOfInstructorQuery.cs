using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllQuizesOfSectionOfInstructorQuery : IQuery<IEnumerable<QuizsToSectionOfInstructorDto>>
    {
        public int SectionId { get; set; }  
        public string InstructorUserName {  get; set; } 
    }
}
