using Application.Common.Interfaces;
using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
 
using Domain.Models;

 
namespace Application.CQRS.Query.Faculties
{
    public class GetAllGroupsHandler : IQueryHandler<GetAllFacultiesQuery, IEnumerable<Faculty>>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetAllGroupsHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<IEnumerable<Faculty>>> Handle(GetAllFacultiesQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var Faculties = await unitOfwork.FacultyRepository.GetAllAsync(); 
				return  Result.Create<IEnumerable<Faculty>>(Faculties);
			}
			catch(Exception ex) 
			{
				return Result.Failure<IEnumerable<Faculty>>(new Error(code:"GetAllFaculties", message :ex.Message.ToString()));
			}
		}
	}
}
