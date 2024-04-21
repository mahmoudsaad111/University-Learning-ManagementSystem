using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesOfAcadimicYearQuery : IQuery<IEnumerable<CourseLessInfoDto>>
    {
        public int AcadimicYearId {  get; set; }
        public int CourseCategoryId {  get; set; }  
    }
}
