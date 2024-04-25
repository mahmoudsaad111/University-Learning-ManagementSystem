using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.CourseCycles
{
    public class GetLessInfoCourseCycleQuery :IQuery<IEnumerable<NameIdDto>>
    {
        public int CourseId { get; set; }
        public int GroupId { set; get; }
    }
}
