using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Lectures
{
	public class DeleteLectureHandler : ICommandHandler<DeleteLectureCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteLectureHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteLectureCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Lecture lecture = await unitOfwork.LectureRepository.GetByIdAsync(request.Id);
				if (lecture == null)
					return Result.Failure<int>(new Error(code: "Delete Lecture", message: "No lecture has this Id")) ;


                User user = await unitOfwork.UserRepository.GetUserByUserName(request.CreatorUserName);
                if (user is null)
                    return Result.Failure<int>(new Error(code: "Delete Lecture", message: "Not valid data"));

                bool ifUserHasAcsses = false;
                if (lecture.CourseCycleId != null)
                    ifUserHasAcsses = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId: user.Id, CourseCycleId: (int)lecture.CourseCycleId);
                if (lecture.SectionId != null)
                    ifUserHasAcsses = await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: user.Id, SectionId: (int)lecture.SectionId);


                if (!ifUserHasAcsses)
                    return Result.Failure<int>(new Error(code: "Delete Lecture", message: "not valid data"));


                bool IsDeleted = await unitOfwork.LectureRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Lecture", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Lecture", message: ex.Message.ToString()));
			}
		}
	}
}
