using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Sections;
using Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetAllSectionsOfCourseCycleHandler : IQueryHandler<GetAllSectionsOfCourseCycleQuery, IEnumerable<SectionOfCourseCycleDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllSectionsOfCourseCycleHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<SectionOfCourseCycleDto>>> Handle(GetAllSectionsOfCourseCycleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SectionOfCourseCycle =await unitOfwork.SectionRepository.GetSectionsOfCourseCycle(request.CourseCycleId);
                return Result.Success<IEnumerable<SectionOfCourseCycleDto>> (SectionOfCourseCycle); 
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<SectionOfCourseCycleDto>>(new Error(code: "GetAllSectionsOfCourseCycle", message: ex.Message.ToString()));

            }
        }
    }
}
