using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.AssignementAnswers
{
    public class DeleteAssignementAnswerHandler : ICommandHandler<DeleteAssignementAnswerCommand, int>
    {

        private readonly IUnitOfwork unitOfwork;
        public DeleteAssignementAnswerHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(DeleteAssignementAnswerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await unitOfwork.UserRepository.GetUserByUserName(request.StudentUserName);
                if (user is null)
                    return Result.Failure<int>(new Error(code: "Delete AssignementAnswer", message: "not valid data"));

                AssignmentAnswer assignementanswer = await unitOfwork.AssignementAnswerRepository.GetByIdAsync(request.Id);

                if (assignementanswer is null || assignementanswer.StudentId != user.Id)
                    Result.Failure<int>(new Error(code: "Delete AssignementAnswer", message: "No assignementanswer has this Id"));

                bool IsDeleted = await unitOfwork.AssignementAnswerRepository.DeleteAsync(request.Id);

                if (IsDeleted)
                {
                    int NumOfTasks = await unitOfwork.SaveChangesAsync();
                    return Result.Success<int>(request.Id);
                }
                return Result.Failure<int>(new Error(code: "Delete AssignementAnswer", message: "Unable To Delete"));
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "Delete AssignementAnswer", message: ex.Message.ToString()));
            }
        }
    }
}
