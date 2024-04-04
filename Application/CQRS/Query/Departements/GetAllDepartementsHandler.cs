using Application.Common.Interfaces;
using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
 
using Domain.Models;
 
namespace Application.CQRS.Query.Departements
{
    public class GetAllDepartementsHandler : IQueryHandler<GetAllDepartementsQuery, IEnumerable<Departement>>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetAllDepartementsHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<IEnumerable<Departement>>> Handle(GetAllDepartementsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var departements = await unitOfwork.DepartementRepository.FindAllAsyncInclude(  );
				return  Result.Create<IEnumerable<Departement>>(departements);
			}
			catch(Exception ex) 
			{
				return Result.Failure<IEnumerable<Departement>>(new Error(code:"GetAllDepartements", message :ex.Message.ToString()));
			}
		}
	}
}
