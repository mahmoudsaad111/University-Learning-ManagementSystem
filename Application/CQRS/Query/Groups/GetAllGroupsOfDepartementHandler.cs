using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Groups;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Groups
{
    public class GetAllGroupsOfDepartementHandler : IQueryHandler<GetAllGroupsOfDepartementQuery, IEnumerable<GroupLessInfoDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllGroupsOfDepartementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<GroupLessInfoDto>>> Handle(GetAllGroupsOfDepartementQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var GroupsOfDepartements = await unitOfwork.GroupRepository.GetGroupsOfDepartement(request.DepartementId);
                return Result.Success<IEnumerable<GroupLessInfoDto>>(GroupsOfDepartements);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<GroupLessInfoDto>>(new Error(code: "GetAllGroupsOfDepartement", message: ex.Message.ToString()));
            }
        }
    }
}
