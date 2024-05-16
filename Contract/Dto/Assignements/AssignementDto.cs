using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Assignements
{
    public class AssignementDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public int FullMark { get; set; }
        public DateTime EndedAt { get; set; }
        public int SectionId { get; set; }
        public Assignment GetAssignement()
        {
            return new Assignment
            {
                Name = Name,
                Description = Description,
                FullMark = FullMark,
                EndedAt = EndedAt,
                SectionId = SectionId
            };
        }
    }
}
