using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Groups;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Groups
{
    public class GetGroupChatHandler : IQueryHandler<GetGroupChatQuery, IEnumerable<Message>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetGroupChatHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<Message>>> Handle(GetGroupChatQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var Messages = await unitOfwork.GroupRepository.GetGroupChat(request.GroupId);
                return Result.Success<IEnumerable<Message>>(Messages);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Message>>(new Error(code: "GetAllGroupsOfDepartement", message: ex.Message.ToString()));
            }
        }
    }
}
