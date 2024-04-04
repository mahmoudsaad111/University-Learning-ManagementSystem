using Application.Common.Interfaces.CQRSInterfaces; 
using Contract.Dto.Groups;
 
 
namespace Application.CQRS.Command.Groups
{
	public class DeleteGroupCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public GroupDto GroupDto { get; set; }
	}
}
