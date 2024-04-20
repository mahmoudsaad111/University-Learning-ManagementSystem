using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Contract.Dto.Departements;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Departements
{
    public class GetDepartementBelongsToFacultyHandler : IQueryHandler<GetDepartementBelongsToFacultyQuery, IEnumerable<Departement>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetDepartementBelongsToFacultyHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<Departement>>> Handle(GetDepartementBelongsToFacultyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Departements = await unitOfwork.DepartementRepository.GetDepartementsBelongsToFaculty(request.FacultyId);
                return Result.Success<IEnumerable<Departement>>(Departements);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Departement>>(new Error( code: "" , message :" "));
            }
        }
    }
}
