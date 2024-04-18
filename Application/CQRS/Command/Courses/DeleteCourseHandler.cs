using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Courses
{
	public class DeleteCourseHandler : ICommandHandler<DeleteCourseCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteCourseHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Course course = await unitOfwork.CourseRepository.GetByIdAsync(request.Id);
				if (course == null)
					Result.Failure<int>(new Error(code: "Delete Course", message: "No course has this Id")) ;

				//if (
				//	 (course.TotalMark != request.CourseDto.TotalMark) ||
				//	 (course.Name!=request.CourseDto.Name) ||
				//	 (course.CourseCategoryId)!=(request.CourseDto.CourseCategoryId)
				//	) 
				//{
				//	return Result.Failure<int>(new Error(code: "Delete Course", message: "Data of the course is not the same in database"));
				//}

				bool IsDeleted = await unitOfwork.CourseRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Course", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Course", message: ex.Message.ToString()));
			}
		}
	}
}
