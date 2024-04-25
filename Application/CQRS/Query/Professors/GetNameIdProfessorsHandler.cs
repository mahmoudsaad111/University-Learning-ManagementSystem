using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Professors
{
    public class GetNameIdProfessorsHandler : IQueryHandler<GetNameIdProfessorsQuery, IEnumerable<NameIdDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetNameIdProfessorsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<NameIdDto>>> Handle(GetNameIdProfessorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var NameIdProfessors = await unitOfwork.ProfessorRepository.GetLessInfoProfessorByDeptId(request.DepartementId);
                return Result.Success<IEnumerable<NameIdDto>>(NameIdProfessors);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<NameIdDto>>(new Error(code: "GetNameIdProfessor", message: ex.Message.ToString()));
            }
        }
    }
}
