using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Assignements
{
	public class UpdateDepartementHandler : ICommandHandler<UpdateAssignementCommand, Assignment>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateDepartementHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Assignment>> Handle(UpdateAssignementCommand request, CancellationToken cancellationToken)
		{
			try
			{

				Assignment assignement = await unitOfwork.AssignementRepository.FindAsync(f => f.AssignmentId == request.Id);
				if (assignement is null)
					return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "No Assignement exist by this Id"));

				if (assignement.SectionId != request.AssignementDto.SectionId)
					return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "Can not change the SectionId")) ;

                assignement.Name = request.AssignementDto.Name;
				assignement.Description = request.AssignementDto.Description;
				assignement.FullMark = request.AssignementDto.FullMark;
				assignement.DeadLine=request.AssignementDto.DeadLine;
				

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Assignment>(assignement);
			}
			catch (Exception ex)
			{
				return Result.Failure<Assignment>(new Error(code: "Updated Assignement" , message: ex.Message.ToString())); 
			}
		}
	}
}
