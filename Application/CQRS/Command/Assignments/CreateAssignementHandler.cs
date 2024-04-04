using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Assignements;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Assignements
{
    public class CreateAssignementHandler : ICommandHandler<CreateAssignementCommand, Assignment>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreateAssignementHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<Assignment>> Handle(CreateAssignementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Assignment assignement = await unitOfwork.AssignementRepository.CreateAsync(request.AssignementDto.GetAssignement());
                if (assignement is null)
                    return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<Assignment>(assignement);
            }
            catch (Exception ex)
            {
                return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: ex.Message.ToString()));
            }
        }
    }
}
