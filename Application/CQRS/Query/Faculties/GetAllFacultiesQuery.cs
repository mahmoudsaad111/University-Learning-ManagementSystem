using Application.Common.Interfaces.CQRSInterfaces;
 
using Domain.Models;
 
namespace Application.CQRS.Query.Faculties
{
    public class GetAllFacultiesQuery : IQuery<IEnumerable<Faculty>>
	{
		
	}
}
