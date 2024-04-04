using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Courses;
using Domain.Models;


namespace Application.CQRS.Command.Courses
{
	public class CreateCourseCommand : ICommand<Course>
	{
		public CourseDto CourseDto { get; set; }
	}
}
