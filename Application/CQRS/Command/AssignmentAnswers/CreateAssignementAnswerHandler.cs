using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.AssignementAnswers;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.AssignementAnswers
{
    public class CreateAssignementAnswerHandler : ICommandHandler<CreateAssignementAnswerCommand, AssignmentAnswer>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreateAssignementAnswerHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<AssignmentAnswer>> Handle(CreateAssignementAnswerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AssignmentAnswer assignementanswer = await unitOfwork.AssignementAnswerRepository.CreateAsync(request.AssignementAnswerDto.GetAssignementAnswer());
                if (assignementanswer is null)
                    return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<AssignmentAnswer>(assignementanswer);
            }
            catch (Exception ex)
            {
                return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: ex.Message.ToString()));
            }
        }
    }
}
