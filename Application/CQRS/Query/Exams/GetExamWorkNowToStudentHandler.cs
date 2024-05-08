using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Domain.Models;
using Domain.Shared;

using Domain.Enums;
namespace Application.CQRS.Query.Exams
{
    public class GetExamWorkNowToStudentHandler : IQueryHandler<GetExamWorkNowToStudentQuery, ExamWrokNowDto>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetExamWorkNowToStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<ExamWrokNowDto>> Handle(GetExamWorkNowToStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Exam ExamWorkNow=  await unitOfwork.ExamRepository.GetExamIncludinigExamPlaceByExamId(request.ExamId);
                if (ExamWorkNow == null)
                    return Result.Failure<ExamWrokNowDto>(new Error(code: "Get Exam Work Now", message: "Invalid ExamId"));

                if(DateTime.Now < ExamWorkNow.StratedAt || DateTime.Now > (ExamWorkNow.StratedAt+ExamWorkNow.DeadLine))
                    return Result.Failure<ExamWrokNowDto>(new Error(code: "Get Exam Work Now", message: "can not get exam at this time"));

                ExamPlace examPlace = ExamWorkNow.ExamPlace;
                bool IfStudentHasAccessToExam = false; 
                if (examPlace is null)
                    return Result.Failure<ExamWrokNowDto>(new Error(code: "Get Exam Work Now", message: "wrong examId")) ;
      
                if (examPlace.ExamType == ExamType.Quiz && examPlace.SectionId != 0)
                    IfStudentHasAccessToExam = await unitOfwork.StudentSectionRepository.CheckIfStudnetInSectionByUserName(
                        StudentUserName: request.StudentUserName, SectionId: (int)(examPlace.SectionId));
            
                else if ((examPlace.ExamType == ExamType.Quiz || examPlace.ExamType==ExamType.Midterm) && examPlace.CourseCycleId is not null)
                    IfStudentHasAccessToExam = await unitOfwork.StudentCourseCycleRepository.CheckIfStudnetInCourseCycle(
                        StudnetUserName :  request.StudentUserName, CourseCylceId : (int)(examPlace.CourseCycleId));

                else if((examPlace.ExamType == ExamType.Semester || examPlace.ExamType == ExamType.Final) && examPlace.CourseId is not null)
                    IfStudentHasAccessToExam= await unitOfwork.CourseRepository.CheckIfStudentHasAccessToCourse(
                        StudentUserName:request.StudentUserName , CourseId :  (int)(examPlace.CourseId));


                if(!IfStudentHasAccessToExam)
                    return Result.Failure<ExamWrokNowDto>(new Error(code: "Get Exam Work Now", message: "Student has no access to exam"));

                ExamWrokNowDto examWrokNowDto = await unitOfwork.ExamRepository.GetExamWorkNow(request.ExamId); 

                if(examWrokNowDto!=null)    
                    return Result.Success<ExamWrokNowDto> (examWrokNowDto);

                return Result.Failure<ExamWrokNowDto>(new Error(code: "Get Exam Work Now", message: "Can not laod the exam"));


            }
            catch (Exception ex)
            {
                return Result.Failure<ExamWrokNowDto>(new Error(code: "Get Exam Work Now", message: ex.Message.ToString())) ;
            }
        }
    }
}
