using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Students
{
    public class GetAllStudentsOfDepartementAndAcadimicYearHandler : IQueryHandler<GetAllStudentsOfDepartementAndAcadimicYearQuery, IEnumerable<ReturnedStudentDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllStudentsOfDepartementAndAcadimicYearHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<ReturnedStudentDto>>> Handle(GetAllStudentsOfDepartementAndAcadimicYearQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Students = await unitOfwork.StudentRepository.GetAllStudentsInDepartementAndAcadimicYear( AcadimicYearId: request.AcadimicYearId);
                return Result.Success<IEnumerable<ReturnedStudentDto>>(Students);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<ReturnedStudentDto>>(new Error(code: "GetAllStudentsOfDepartementAndAcadimicYear", message: ex.Message.ToString()));
            }
           
        }
    }
}
