using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;
 
 
namespace Application.CQRS.Query.Departements
{
    public class GetAllDepartementsQuery : IQuery<IEnumerable<Departement>>
	{
		
	}
}
