using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetQuizOfSectionOfInstructorHandler : IQueryHandler<GetAllQuizesOfSectionOfInstructorQuery, IEnumerable<QuizsToSectionOfInstructorDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetQuizOfSectionOfInstructorHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<QuizsToSectionOfInstructorDto>>> Handle(GetAllQuizesOfSectionOfInstructorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Section = await unitOfwork.SectionRepository.GetByIdAsync(request.SectionId);
                if (Section is not null)
                {
                    var User = await unitOfwork.UserRepository.GetUserByUserName(request.InstructorUserName);
                    if (User is not null)
                    {
                        if(Section.InstructorId == User.Id)
                        {
                            var ResultOfGetQuizsToSectionOfInstructor = await unitOfwork.ExamRepository.GetAllQuizsToSectionOfInstructors(sectionId: request.SectionId);
                            return Result.Success<IEnumerable<QuizsToSectionOfInstructorDto>>(ResultOfGetQuizsToSectionOfInstructor);
                        }
                        return Result.Failure <IEnumerable<QuizsToSectionOfInstructorDto>>(new Error(code: "QuizOfSection", message: "this user has no access"));
                    }
                }
                return Result.Failure<IEnumerable<QuizsToSectionOfInstructorDto>>(new Error(code: "QuizOfSection", message: "wrong SectionId"));
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<QuizsToSectionOfInstructorDto>>(new Error(code: "QuizOfSection", message: ex.Message.ToString()));
            }
        }
    }
}
