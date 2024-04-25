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
    public class GetAllSectionsOfInstructorHandler : IQueryHandler<GetAllSectionsOfInstructorQuery, IEnumerable<SectionOfInstructorDto>>
    {
        private readonly IUnitOfwork unitOfWork;

        public GetAllSectionsOfInstructorHandler(IUnitOfwork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<SectionOfInstructorDto>>> Handle(GetAllSectionsOfInstructorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SectionsOfInstructor = await unitOfWork.SectionRepository.GetSectionsOfInstructor(request.InstructorId);
                return Result.Success<IEnumerable<SectionOfInstructorDto>>(SectionsOfInstructor);

            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<SectionOfInstructorDto>>(new Error(code: "GetAllSectionsOfInstructor", message: ex.Message.ToString()));
            }
        }
    }
}
