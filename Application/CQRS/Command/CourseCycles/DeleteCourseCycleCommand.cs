using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCycles;
 

namespace Application.CQRS.Command.CourseCycles
{
	public class DeleteCourseCycleCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public CourseCycleDto CourseCycleDto { get; set; }
	}
}
