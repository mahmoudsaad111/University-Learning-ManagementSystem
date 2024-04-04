using Application.Common.Interfaces;
using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
 
namespace Application.CQRS.Query.Groups
{
    public class GetAllGroupsHandler : IQueryHandler<GetAllGroupsQuery, IEnumerable<Group>>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetAllGroupsHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<IEnumerable<Group>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var departements = await unitOfwork.GroupRepository.FindAllAsyncInclude();
				return  Result.Create<IEnumerable<Group>>(departements);
			}
			catch(Exception ex) 
			{
				return Result.Failure<IEnumerable<Group>>(new Error(code:"GetAllGroups", message :ex.Message.ToString()));
			}
		}
	}
}
