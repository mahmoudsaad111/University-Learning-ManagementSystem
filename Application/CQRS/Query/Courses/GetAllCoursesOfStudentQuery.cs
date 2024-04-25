using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesOfStudentQuery :IQuery<IEnumerable<CourseOfStudentDto>>
    {
        public int StudentId { get; set; }
    //    public int AcadimicYearId { get; set; }
    }
}
