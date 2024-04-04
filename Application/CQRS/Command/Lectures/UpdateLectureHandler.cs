using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Lectures
{
	public class UpdateLecturetHandler : ICommandHandler<UpdateLectureCommand, Lecture>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateLecturetHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Lecture>> Handle(UpdateLectureCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Lecture lecture = unitOfwork.LectureRepository.Find(f => f.LectureId == request.Id);
				if (lecture is null)
					return Result.Failure<Lecture>(new Error(code: "Update Lecture", message: "No Lecture exist by this Id"));
				
				if(
					(request.LectureDto.SectionId==0 && request.LectureDto.CourseCycleId==0) || 
					(request.LectureDto.SectionId!=0 && request.LectureDto.CourseCycleId!=0)
				   )
					return Result.Failure<Lecture>(new Error(code:"Update Lecture" , message :"Lecture should be belongs to only one CourseCycle or only one Section"));

				var GetLectureFromLectureDto = request.LectureDto.GetLecture();
				lecture.Name = GetLectureFromLectureDto.Name;
				lecture.SectionId = GetLectureFromLectureDto.SectionId;
				lecture.CourseCycleId = GetLectureFromLectureDto.CourseCycleId;
				lecture.HavingAssignment = GetLectureFromLectureDto.HavingAssignment;
				 
				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Lecture>(lecture);
			}
			catch (Exception ex)
			{
				return Result.Failure<Lecture>(new Error(code: "Updated Lecture" , message: ex.Message.ToString())); 
			}
		}
	}
}
