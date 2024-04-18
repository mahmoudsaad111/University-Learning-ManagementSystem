using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Courses
{
	public class UpdateDepartementHandler : ICommandHandler<UpdateCourseCommand, Course>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateDepartementHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Course>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
		{
			try
			{

				Course course = unitOfwork.CourseRepository.Find(f => f.CourseId == request.Id);
				if (course is null)
					return Result.Failure<Course>(new Error(code: "Update Course", message: "No Course exist by this Id"));

				course.AcadimicYearId = request.courseDto.AcadimicYearId; 
				course.Name = request.courseDto.Name;
				course.CourseCategoryId = request.courseDto.CourseCategoryId;
				course.Description = request.courseDto.Description;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Course>(course);
			}
			catch (Exception ex)
			{
				return Result.Failure<Course>(new Error(code: "Updated Course" , message: ex.Message.ToString())); 
			}
		}
	}
}
