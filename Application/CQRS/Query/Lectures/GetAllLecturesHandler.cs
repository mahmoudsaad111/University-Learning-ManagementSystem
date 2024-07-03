using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Query.Lectures;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Lectures
{
    public class GetAllLecturesHandler : IQueryHandler<GetAllLecturesQuery , IEnumerable<Lecture>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllLecturesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<Lecture>>> Handle(GetAllLecturesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if ((request.GetLectureDto.IsProfessor && request.GetLectureDto.CourseCycleId == 0) ||
                    (!request.GetLectureDto.IsProfessor && request.GetLectureDto.SectionId == 0))
                {
                    return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesHandler", message: "Wrong data"));
                }

                bool IfUSerHasAccess = false; 
                User user=await unitOfwork.UserRepository.GetUserByUserName(request.GetLectureDto.CreatorUserName);
                if(user is null)
                    return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesHandler", message: "Not valid data"));

                if (request.GetLectureDto.IsProfessor)
                {
                    IfUSerHasAccess = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId:user.Id , CourseCycleId: request.GetLectureDto.CourseCycleId);
                }
                else
                {
                    IfUSerHasAccess = await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: user.Id, SectionId: request.GetLectureDto.SectionId);
                }

                IEnumerable<Lecture> Lectures = new List<Lecture>();
                if(IfUSerHasAccess)
                {
                    if (request.GetLectureDto.IsProfessor)
                        Lectures = await unitOfwork.LectureRepository.GetLecturesOfCourseCycle(CourseCycleId: request.GetLectureDto.CourseCycleId);
                    else Lectures =await unitOfwork.LectureRepository.GetLecturesOfSection(SectionId:request.GetLectureDto.SectionId);

                    return Result.Create<IEnumerable<Lecture>>(Lectures);
                }

                return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLecturesHandler", message: "User Has no access"));

            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Lecture>>(new Error(code: "GetAllLectures", message: ex.Message.ToString()));
            }
        }
    }
}
