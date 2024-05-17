using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Application.CQRS.Query.StudentExams
{
    public class GetEmailsOfStudentsHavingAccessToExamHandler : IQueryHandler<GetEmailsOfStudentsHavingAccessToExamQuery, IEnumerable<string>>
    {
        private readonly IUnitOfwork unitOfwork;
        public GetEmailsOfStudentsHavingAccessToExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<string>>> Handle(GetEmailsOfStudentsHavingAccessToExamQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<string> Emails = new List<string>();

                var Exam = await unitOfwork.ExamRepository.GetExamIncludinigExamPlaceByExamId(request.ExamId);
                if (Exam is null)
                    return Result.Failure<IEnumerable<string>>(new Error(code: "GetEmailsOfStudentsHavingAccessToExam", message: "Invalid data"));
                var ExamPlace = Exam.ExamPlace;
           
                if (ExamPlace is null)
                    return Result.Failure<IEnumerable<string>>(new Error(code: "GetEmailsOfStudentsHavingAccessToExam", message: "Invalid data"));

                if (ExamPlace.ExamType == ExamType.Quiz && ExamPlace.SectionId is not null)
                    Emails = await unitOfwork.StudentSectionRepository.GetEmailsOfStudentsOnSection((int)ExamPlace.SectionId);

                else if ((ExamPlace.ExamType == ExamType.Midterm || ExamPlace.ExamType == ExamType.Quiz) && ExamPlace.CourseCycleId is not null)
                    Emails = await unitOfwork.StudentCourseCycleRepository.GetEmailsOfStudentsOnCourseCycle(CourseCycleId: (int)ExamPlace.CourseCycleId);

                else if( (ExamPlace.ExamType==ExamType.Semester||ExamPlace.ExamType==ExamType.Final) && ExamPlace.CourseId is not null )
                    Emails=await unitOfwork.CourseRepository.GetEmailsOfStudnetsHavingAccessToCourseOnAllGroups(CourseId :(int) ExamPlace.CourseId);

                return Result.Success(Emails);
                    
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error(code: "GetEmailsOfStudentsHavingAccessToExam", message: ex.Message.ToString()));
            }

        }
    }
}
