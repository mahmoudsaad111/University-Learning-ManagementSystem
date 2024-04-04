using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.CourseCycles
{
	public class UpdateCourseCycleHandler : ICommandHandler<UpdateCourseCycleCommand, CourseCycle>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateCourseCycleHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<CourseCycle>> Handle(UpdateCourseCycleCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CourseCycle courseCycle = unitOfwork.CourseCycleRepository.Find(f => f.CourseCycleId == request.Id);
				if (courseCycle is null)
					return Result.Failure<CourseCycle>(new Error(code: "Update CourseCycle", message: "No CourseCycle exist by this Id"));
				
				courseCycle.CourseId = request.CourseCycleDto.CourseId;
				courseCycle.ProfessorId = request.CourseCycleDto.ProfessorId;
				courseCycle.GroupId = request.CourseCycleDto.GroupId;
				courseCycle.Title = request.CourseCycleDto.Title;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<CourseCycle>(courseCycle);
			}
			catch (Exception ex)
			{
				return Result.Failure<CourseCycle>(new Error(code: "Updated CourseCycle" , message: ex.Message.ToString())); 
			}
		}
	}
}
