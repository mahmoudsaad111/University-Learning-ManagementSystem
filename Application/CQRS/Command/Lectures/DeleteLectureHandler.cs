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

				var GetLectureFromLectureDto = request.LectureDto.GetLecture();

                if (
					(lecture.CourseCycleId != GetLectureFromLectureDto.CourseCycleId) || 
					 (lecture.SectionId != GetLectureFromLectureDto.SectionId) ||
					 (lecture.Name != GetLectureFromLectureDto.Name) ||
					 (lecture.HavingAssignment!=GetLectureFromLectureDto.HavingAssignment)
					) 
				{
					return Result.Failure<int>(new Error(code: "Delete Lecture", message: "Data of the lecture is not the same in database"));
				}

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
