using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.CQRS.Query.Groups
{
    public class GetAllGroupsOfAcadimicYearHandler : IQueryHandler<GetAllGroupsOfAcadimicYearQuery, IEnumerable<NameIdDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllGroupsOfAcadimicYearHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<NameIdDto>>> Handle(GetAllGroupsOfAcadimicYearQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var GroupsLessInfo =await unitOfwork.GroupRepository.GetLessInfoGroupsOfAcadimicYear(request.AcadimicYearId);
                return Result.Success<IEnumerable<NameIdDto>>(GroupsLessInfo);  
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<NameIdDto>>(new Error(code: "GetAllGroupsOfAcadimicYear", message: ex.Message.ToString()));
            }
        }
    }
}
