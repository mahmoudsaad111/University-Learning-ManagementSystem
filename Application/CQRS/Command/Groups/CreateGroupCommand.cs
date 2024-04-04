using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Groups;
using Domain.Models;
 
namespace Application.CQRS.Command.Groups
{
	public class CreateGroupCommand :ICommand <Group>
	{
		public GroupDto groupDto { get; set; }
	}
}
