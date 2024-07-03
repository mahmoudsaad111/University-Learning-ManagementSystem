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

				if ( 
					 (lecture.CourseCycleId!=null && request.LectureDto.CourseCycleId!=lecture.CourseCycleId)	 || 
					 (lecture.SectionId!=null && request.LectureDto.SectionId!=lecture.SectionId)
					)
                    return Result.Failure<Lecture>(new Error(code: "Update Lecture", message: "not valid data"));


                User user = await unitOfwork.UserRepository.GetUserByUserName(request.CreatorUserName);
                if (user is null)
                    return Result.Failure<Lecture>(new Error(code: "Update Lecture", message: "Not valid data"));


                bool ifUserHasAcsses = false;
                if (lecture.CourseCycleId != null)
                    ifUserHasAcsses = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId: user.Id, CourseCycleId: (int)lecture.CourseCycleId);
                if (lecture.SectionId != null)
                    ifUserHasAcsses = await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: user.Id, SectionId: (int)lecture.SectionId);


                if (!ifUserHasAcsses)
                    return Result.Failure<Lecture>(new Error(code: "Update Lecture", message: "not valid data"));

                var GetLectureFromLectureDto = request.LectureDto.GetLecture();
				lecture.Name = GetLectureFromLectureDto.Name;
			//	lecture.SectionId = GetLectureFromLectureDto.SectionId;
			//	lecture.CourseCycleId = GetLectureFromLectureDto.CourseCycleId;
				lecture.HavingAssignment = GetLectureFromLectureDto.HavingAssignment;
				lecture.VedioUrl = request.LectureDto.VideoUrl;
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
