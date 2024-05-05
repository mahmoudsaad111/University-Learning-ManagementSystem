using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;

namespace Application.CQRS.Command.Exams
{
    public class DeleteExamHandler : ICommandHandler<DeleteExamCommand, int>
    {
        private readonly IUnitOfwork unitOfwork;

        public DeleteExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle( DeleteExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Exam examWillDeleted = await unitOfwork.ExamRepository.GetByIdAsync(request.ExamId);
                if (examWillDeleted is null)
                   return Result.Failure<int>(new Error(code: "DeleteExam", message: "No Exam has this Id"));

                bool IfUserHasAccessToExam = false;

                ExamPlace examPlaceOfCurrentExam = await unitOfwork.ExamPlaceRepository.GetByIdAsync(examWillDeleted.ExamPlaceId);
                var user = await unitOfwork.UserRepository.GetUserByUserName(request.ExamCreatorUserName);

                if (user is null)
                    return Result.Failure<int>(new Error(code: "DeleteExam", message: "user has no access"));


                if (examPlaceOfCurrentExam is null)
                    return Result.Failure<int>(new Error(code: "DeleteExam", message: "can not delete this exam"));

                if(examPlaceOfCurrentExam.ExamType==ExamType.Quiz && examPlaceOfCurrentExam.SectionId is not null)
                {
                    IfUserHasAccessToExam= await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: user.Id, SectionId:(int) examPlaceOfCurrentExam.SectionId);
                }
                else if( (examPlaceOfCurrentExam.ExamType== ExamType.Quiz ||   examPlaceOfCurrentExam.ExamType == ExamType.Midterm) && examPlaceOfCurrentExam.CourseCycleId is not null)
                {
                    IfUserHasAccessToExam = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId: user.Id,CourseCycleId: (int)examPlaceOfCurrentExam.CourseCycleId);
                }
                else if(examPlaceOfCurrentExam.ExamType==ExamType.Final || examPlaceOfCurrentExam.ExamType == ExamType.Semester)
                {
                    IfUserHasAccessToExam = true;
                }

                if(!IfUserHasAccessToExam)
                    return Result.Failure<int>(new Error(code: "DeleteExam", message: "user has no access"));


                bool Delted = await unitOfwork.ExamRepository.DeleteAsync(request.ExamId);
                if (Delted)
                {
                    await unitOfwork.SaveChangesAsync();
                    return Result.Success<int>(request.ExamId);
                }

                return Result.Failure<int>(new Error(code: "DeleteExam", message: "No Exam has this Id")) ;
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "DeleteExam", message: ex.Message.ToString()) );
            }
        }
    }
}
