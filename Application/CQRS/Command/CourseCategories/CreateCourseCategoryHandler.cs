using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.CourseCategories
{
	public class CreateCourseCategoryHandler : ICommandHandler<CreateCourseCategoryCommand, CourseCategory>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateCourseCategoryHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<CourseCategory>> Handle(CreateCourseCategoryCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CourseCategory courseCategory=await unitOfwork.CourseCategoryRepository.CreateAsync(request.CourseCategoryDto.GetCourseCategory());
				if (courseCategory == null)
					return Result.Failure<CourseCategory>(new Error(code: "Create CourseCategory", message: "Uanble to create couresCategory"));
				await unitOfwork.SaveChangesAsync();
				return Result.Success<CourseCategory>(courseCategory);
			}
			catch (Exception ex)
			{
				return Result.Failure<CourseCategory>(new Error(code: "Create CourseCategory" , message: ex.Message.ToString()));	
			}
		}
	}
}
