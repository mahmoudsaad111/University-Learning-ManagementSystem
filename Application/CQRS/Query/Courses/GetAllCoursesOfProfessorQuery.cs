using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesOfProfessorQuery : IQuery<IEnumerable<CourseOfProfessorDto>>
    {
        public int ProfessorId {  get; set; }
    }
}
