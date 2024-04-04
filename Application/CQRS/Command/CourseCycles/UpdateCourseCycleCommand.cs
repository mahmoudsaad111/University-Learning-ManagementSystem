using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCycles;
 
using Domain.Models;
 

namespace Application.CQRS.Command.CourseCycles
{
	public class UpdateCourseCycleCommand  : ICommand<CourseCycle>
	{
		public int Id { get; set; }
		public CourseCycleDto CourseCycleDto { get; set; }
	}
}
