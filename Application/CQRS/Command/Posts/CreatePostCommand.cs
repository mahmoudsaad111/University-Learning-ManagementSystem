using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Posts;
using Domain.Models;


namespace Application.CQRS.Command.Posts
{
	public class CreatePostCommand : ICommand<Post>
	{
		public PostDto PostDto { get; set; }
	}
}
