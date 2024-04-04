using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.CourseCycles
{
	public class DeleteCourseCycleHandler : ICommandHandler<DeleteCourseCycleCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteCourseCycleHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteCourseCycleCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CourseCycle courseCycle = await unitOfwork.CourseCycleRepository.GetByIdAsync(request.Id);
				if (courseCycle == null)
					return Result.Failure<int>(new Error(code: "Delete CourseCycle", message: "No courseCycle has this Id")) ;

				if ((courseCycle.CourseId != request.CourseCycleDto.CourseId) ||
					 (courseCycle.GroupId != request.CourseCycleDto.GroupId) ||
					 (courseCycle.ProfessorId != request.CourseCycleDto.ProfessorId) ||
					 (courseCycle.Title!=request.CourseCycleDto.Title) 
					) 
				{
					return Result.Failure<int>(new Error(code: "Delete CourseCycle", message: "Data of the courseCycle is not the same in database"));
				}

				bool IsDeleted = await unitOfwork.CourseCycleRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete CourseCycle", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete CourseCycle", message: ex.Message.ToString()));
			}
		}
	}
}
