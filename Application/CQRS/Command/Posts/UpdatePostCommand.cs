using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Posts;
 
using Domain.Models;
 

namespace Application.CQRS.Command.Posts
{
	public class UpdatePostCommand  : ICommand<Post>
	{
		public int Id { get; set; }
		public PostDto PostDto { get; set; }
	}
}
