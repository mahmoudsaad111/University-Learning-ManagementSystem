using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
 
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Lectures
{
    public class CreateLectureHandler : ICommandHandler<CreateLectureCommand, Lecture>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreateLectureHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<Lecture>> Handle(CreateLectureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (
                    (request.LectureDto.SectionId == 0 && request.LectureDto.CourseCycleId == 0) ||
                    (request.LectureDto.SectionId != 0 && request.LectureDto.CourseCycleId != 0)
                   )
                    return Result.Failure<Lecture>(new Error(code: "Update Lecture", message: "Lecture should be belongs to only one CourseCycle or only one Section"));

                Lecture lecture = await unitOfwork.LectureRepository.CreateAsync(request.LectureDto.GetLecture());
                if (lecture is null)
                    return Result.Failure<Lecture>(new Error(code: "Create Lecture", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<Lecture>(lecture);
            }
            catch (Exception ex)
            {
                return Result.Failure<Lecture>(new Error(code: "Create Lecture", message: ex.Message.ToString()));
            }
        }
    }
}
