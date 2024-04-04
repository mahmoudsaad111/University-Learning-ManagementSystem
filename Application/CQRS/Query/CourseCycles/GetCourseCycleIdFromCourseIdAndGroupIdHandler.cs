using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.CourseCycles
{
    public class GetCourseCycleIdFromCourseIdAndGroupIdHandler : IQueryHandler<GetCourseCycleIdFromCourseIdAndGroupIdQuery, CourseCycle>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetCourseCycleIdFromCourseIdAndGroupIdHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<CourseCycle>> Handle(GetCourseCycleIdFromCourseIdAndGroupIdQuery request, CancellationToken cancellationToken)
        {
            CourseCycle courseCycle = await  unitOfwork.CourseCycleRepository.GetCourseCycleUsingCourseIdAndGroupIdAsync(
                courseId : request.CourseId, groupId:request.GroupId);
 
                return courseCycle;
              
        }
    }
}
