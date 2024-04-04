using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Courses;
 

namespace Application.CQRS.Command.Courses
{
	public class DeleteCourseCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public CourseDto CourseDto { get; set; }
	}
}
