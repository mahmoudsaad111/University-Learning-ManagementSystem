using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Assignements
{
    public class AssignemntFilesDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public int FullMark { get; set; }
        public DateTime EndedAt { get; set; }
        public IEnumerable<AssignmentResource> ResourcesOfAssigenments { get; set; }    
    }
}
