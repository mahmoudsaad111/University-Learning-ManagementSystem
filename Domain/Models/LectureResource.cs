using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LectureResource :FileResource
    {
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; } = null!; 
    }
}
