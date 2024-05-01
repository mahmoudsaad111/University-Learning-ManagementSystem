using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.StudentExamDto;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.StudentExams
{
    public class GetExamAnswerOfStudentHandler : IQueryHandler<GetExamAnswerOfStudentQuery, StudentAnswersOfExamDto>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetExamAnswerOfStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<StudentAnswersOfExamDto>> Handle(GetExamAnswerOfStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var StudentExamAnswer = await unitOfwork.StudentExamRepository.GetStudentsAnswerOfExamWithModelAnswer(
                    studentUserName: request.StudentUserName,
                    ExamId: request.ExamId);

                if (StudentExamAnswer is null)
                    return Result.Failure<StudentAnswersOfExamDto>(new Error(code: "StudentAnswersOfExamDto", message: "unable to load student Answer"));

                return Result.Success<StudentAnswersOfExamDto>(StudentExamAnswer);

            }
            catch (Exception ex)
            {
                return Result.Failure<StudentAnswersOfExamDto>(new Error(code: "StudentAnswersOfExamDto", message: ex.Message.ToString())) ;

            }
        }
    }
}
