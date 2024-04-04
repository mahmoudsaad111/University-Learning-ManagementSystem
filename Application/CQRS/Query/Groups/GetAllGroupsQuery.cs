using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Faculties;
using Domain.Models;
 
namespace Application.CQRS.Query.Groups
{
    public class GetAllGroupsQuery : IQuery<IEnumerable<Group>>
	{
		
	}
}
