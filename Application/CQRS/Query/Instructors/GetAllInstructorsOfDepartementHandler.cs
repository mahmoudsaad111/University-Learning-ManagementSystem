using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Instructors
{
    public class GetAllInstructorsOfDepartementHandler : IQueryHandler<GetAllInstructorsOfDepartementQuery, IEnumerable<ReturnedInstructorDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllInstructorsOfDepartementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<ReturnedInstructorDto>>> Handle(GetAllInstructorsOfDepartementQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Instrucors = await unitOfwork.InstructorRepository.GetAllInstructorInDepartement(request.DepartementId);
                return Result.Success<IEnumerable<ReturnedInstructorDto>> (Instrucors);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<ReturnedInstructorDto>>(new Error(code: "GetAllInstructorsOfDepartement", message: ex.Message.ToString()));
            }
        }
    }
}
