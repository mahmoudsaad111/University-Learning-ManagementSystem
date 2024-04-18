using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Departements
{
    public class GetDepartementBelongsToFacultyHandler : IQueryHandler<GetDepartementBelongsToFacultyQuery, IEnumerable<NameIdDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetDepartementBelongsToFacultyHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<NameIdDto>>> Handle(GetDepartementBelongsToFacultyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Departements = await unitOfwork.DepartementRepository.GetLessInfoDepartementsBelongsToFaculty(request.FacultyId);
                return Result.Success<IEnumerable<NameIdDto>>(Departements);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<NameIdDto>>(new Error( code: "" , message :" "));
            }
        }
    }
}
