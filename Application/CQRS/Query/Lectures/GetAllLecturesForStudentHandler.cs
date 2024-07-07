using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Lectures
{
    public class GetAllLecturesForStudentHandler : IQueryHandler<GetAllLecturesForStudentQuery, IEnumerable<Lecture>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllLecturesForStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<Lecture>>> Handle(GetAllLecturesForStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if ((request.getLectureForStudentDto.CourseCycleId == 0 && request.getLectureForStudentDto.SectionId == 0) ||
                    (request.getLectureForStudentDto.CourseCycleId != 0 && request.getLectureForStudentDto.SectionId != 0))
                {
                    return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesForStudent", message: "Not valid data"));
                }

                var user =await unitOfwork.UserRepository.GetUserByUserName(request.getLectureForStudentDto.StudentUserName);
                if (user is null)
                    return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesForStudent", message: "Not valid data"));

                bool IfHasAccess = false;

                if (request.getLectureForStudentDto.SectionId != 0) 
                    IfHasAccess= await unitOfwork.StudentSectionRepository.CheckIfStudentInSection(StudentId: user.Id,SectionId: request.getLectureForStudentDto.SectionId);
                else
                    IfHasAccess = await unitOfwork.StudentCourseCycleRepository.ChekcIfStudentInCourseCycle (StudentId: user.Id, CourseCycleId : request.getLectureForStudentDto.CourseCycleId       );

                if (IfHasAccess)
                {
                    if (request.getLectureForStudentDto.SectionId != 0)
                        return Result.Success<IEnumerable<Lecture>>(await unitOfwork.LectureRepository.GetLecturesOfSection(SectionId: request.getLectureForStudentDto.SectionId));

                    return Result.Success<IEnumerable<Lecture>>(await unitOfwork.LectureRepository.GetLecturesOfCourseCycle(CourseCycleId: request.getLectureForStudentDto.CourseCycleId));
                }
                return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesForStudent", message: "Has No Access"));
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesForStudent", message: ex.Message.ToString()));
            }
        }
    }
}
