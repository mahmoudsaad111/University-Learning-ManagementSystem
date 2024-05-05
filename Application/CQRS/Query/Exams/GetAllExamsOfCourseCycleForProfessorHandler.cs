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
    public class GetAllExamsOfCourseCycleForProfessorHandler : IQueryHandler<GetAllExamsOfCourseCycleForProfessorQuery, IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllExamsOfCourseCycleForProfessorHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>> Handle(GetAllExamsOfCourseCycleForProfessorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CourseCycle = await unitOfwork.CourseCycleRepository.GetByIdAsync(request.CourseCycleId);
                if(CourseCycle is not null)
                {
                    var User = await unitOfwork.UserRepository.GetUserByUserName(request.ProfessorUserName);
                    if(User is not null)
                    {
                        if(User.Id == CourseCycle.ProfessorId)
                        {
                            var ResultOfExamsOfCourseCylce = await unitOfwork.ExamRepository.GetAllExamsOfCourseCycleOfProfessor(courseCycleId: request.CourseCycleId);
                            return Result.Success<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>(ResultOfExamsOfCourseCylce);
                        }
                        return Result.Failure<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>(new Error(code: "Get all exams of courseCycle", message: "this user has no access"));
                    }
                    return Result.Failure<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>(new Error(code: "Get all exams of courseCycle", message: "this user not found"));
                }
                return Result.Failure<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>(new Error(code: "Get all exams of courseCycle", message: "this course not found"));
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>(new Error(code: "Get all exams of courseCycle", message: ex.Message.ToString()));

            }
        }
    }
}
