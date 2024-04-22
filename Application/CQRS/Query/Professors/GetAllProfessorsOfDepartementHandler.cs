using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Professors
{
    public class GetAllProfessorsOfDepartementHandler : IQueryHandler<GetAllProfessorsOfDepartementQuery, IEnumerable<ReturnedProfessorDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllProfessorsOfDepartementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<ReturnedProfessorDto>>> Handle(GetAllProfessorsOfDepartementQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Professoes = await unitOfwork.ProfessorRepository.GetAllProfessorsInDepartement(request.DepartementId);
                return Result.Success<IEnumerable<ReturnedProfessorDto>>(Professoes);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<ReturnedProfessorDto>>(new Error(code: "GetAllProfessorsOfDepartement", message: ex.Message.ToString()));
            }
        }
    }
}
