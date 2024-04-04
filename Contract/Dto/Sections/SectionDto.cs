using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Sections
{
    public class SectionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }  
        public int CourseCycleId { get; set; }

        public Section GetSection()
        {
            return new Section
            {
                Name = Name,
                Description = Description,
                InstructorId = InstructorId,
                CourseCycleId = CourseCycleId
            };
        }
    }
}
