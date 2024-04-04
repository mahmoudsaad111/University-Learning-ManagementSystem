using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Courses;
 
using Domain.Models;
 

namespace Application.CQRS.Command.Courses
{
	public class UpdateCourseCommand  : ICommand<Course>
	{
		public int Id { get; set; }
		public CourseDto courseDto { get; set; }
	}
}
