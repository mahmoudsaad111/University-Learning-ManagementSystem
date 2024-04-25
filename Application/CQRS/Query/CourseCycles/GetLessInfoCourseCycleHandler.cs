using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.CourseCycles
{
    public class GetLessInfoCourseCycleHandler : IQueryHandler<GetLessInfoCourseCycleQuery, IEnumerable<NameIdDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetLessInfoCourseCycleHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<NameIdDto>>> Handle(GetLessInfoCourseCycleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var AllLessInfoCourseCycles = await unitOfwork.CourseCycleRepository.GetAllLessInfoCourseCycles(groupId: request.GroupId,
                                                                                                                 courseId: request.CourseId);

                return Result.Success<IEnumerable<NameIdDto>> (AllLessInfoCourseCycles);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<NameIdDto>>(new Error(code: "GetLessInfoCourseCylce", message: ex.Message.ToString()));
            }
        }
    }
}
