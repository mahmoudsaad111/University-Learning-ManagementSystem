using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Sections;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetAllSectionsOfProfessorHandler : IQueryHandler<GetAllSectionsOfProfessorQuery, IEnumerable<SectionsOfProfessorDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllSectionsOfProfessorHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<SectionsOfProfessorDto>>> Handle(GetAllSectionsOfProfessorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SectionsOfProfessor = await unitOfwork.SectionRepository.GetAllSectionsOfProfessor(ProfesssorId: request.ProfessorId);
                return Result.Success<IEnumerable<SectionsOfProfessorDto>>(SectionsOfProfessor);

            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<SectionsOfProfessorDto>>(new Error(code: "GetAllSectionsOfProfessor", message: ex.Message.ToString()));
            }
        }
    }
}
