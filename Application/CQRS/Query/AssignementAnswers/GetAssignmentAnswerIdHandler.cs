using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.AssignementAnswers
{
    public class GetAssignmentAnswerIdHandler : IQueryHandler<GetAssignmentAnswerIdQuery, int>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAssignmentAnswerIdHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(GetAssignmentAnswerIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                User user= await this.unitOfwork.UserRepository.GetUserByUserName(request.StudentUserName);

                if (user == null)
                    return Result.Failure<int>(new Error(code: "GetAssignmentAnswerIdQuery", message: "Invalid data"));

                var AssAns= await unitOfwork.AssignementAnswerRepository.FindAsync(a => a.AssignmentId == request.AssessmentId && a.StudentId == user.Id);

                if(AssAns is null)
                    return Result.Failure<int>(new Error(code: "GetAssignmentAnswerIdQuery", message: "Invalid data"));

                return Result.Success<int>(AssAns.AssignmentAnswer_id);
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "GetAssignmentAnswerIdQuery", message: ex.Message.ToString()));
            }
        }
    }
}
