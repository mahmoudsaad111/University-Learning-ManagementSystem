using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetAllSectionsHandler : IQueryHandler<GetAllSectionsQuery, IEnumerable<Section>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllSectionsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<Section>>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sections = await unitOfwork.SectionRepository.FindAllAsyncInclude();
                return Result.Create<IEnumerable<Section>>(sections);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Section>>(new Error(code: "GetAllSections", message: ex.Message.ToString()));
            }
        }
    }
}
