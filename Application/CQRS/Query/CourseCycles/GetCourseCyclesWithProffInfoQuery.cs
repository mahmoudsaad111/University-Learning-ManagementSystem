using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCycles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.CourseCycles
{
    public class GetCourseCyclesWithProffInfoQuery : IQuery<IEnumerable<CourseCycleWithProfInfoDto>>
    {
        public int CourseId {  get; set; }
        public int GroupId {  set; get; }    
    }
}
