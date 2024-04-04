using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Groups;
 
using Domain.Models;
 
namespace Application.CQRS.Command.Groups
{
	public class UpdateGroupCommand  : ICommand<Group>
	{
		public int Id { get; set; }
		public GroupDto GroupDto { get; set; }
	}
}
