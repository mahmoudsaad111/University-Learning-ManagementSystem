using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCycles;
using Domain.Models;


namespace Application.CQRS.Command.CourseCycles
{
	public class CreateCourseCycleCommand : ICommand<CourseCycle>
	{
		public CourseCycleDto CourseCycleDto { get; set; }
	}
}
