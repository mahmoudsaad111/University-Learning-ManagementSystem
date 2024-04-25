using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Instructors
{
    public class GetNameIdInstructorsHandler : IQueryHandler<GetNameIdInstructorsQuery, IEnumerable<NameIdDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetNameIdInstructorsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<NameIdDto>>> Handle(GetNameIdInstructorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var NameIdInstructors = await unitOfwork.InstructorRepository.GetLessInfoInstructorByDeptId(request.DepartementId);
                return Result.Success<IEnumerable<NameIdDto>>(NameIdInstructors);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<NameIdDto>>(new Error(code: "GetNameIdInstructor", message: ex.Message.ToString()));
            }
        }
    }
}
