using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Posts;
 

namespace Application.CQRS.Command.Posts
{
	public class DeletePostCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public PostDto PostDto { get; set; }
	}
}
