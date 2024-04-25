using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.CourseCycles;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.CourseCycles
{
    public class GetCourseCyclesWithProffInfoHandler : IQueryHandler<GetCourseCyclesWithProffInfoQuery, IEnumerable<CourseCycleWithProfInfoDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetCourseCyclesWithProffInfoHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<CourseCycleWithProfInfoDto>>> Handle(GetCourseCyclesWithProffInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CourseCycleWithProfInfo =await unitOfwork.CourseCycleRepository.GetCourseCylcesWithProfInfo(courseId: request.CourseId,
                                                                                                            groupId: request.GroupId); 
                 return Result.Success<IEnumerable<CourseCycleWithProfInfoDto>>(CourseCycleWithProfInfo);
            }
            catch(Exception ex) 
            {
                return Result.Failure<IEnumerable<CourseCycleWithProfInfoDto>>(new Error(code: "GetNameInfoCourseCyclesWithProf", message: ex.Message.ToString()));

            }

        }
    }
}
