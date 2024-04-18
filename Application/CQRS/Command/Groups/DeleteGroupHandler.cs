using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Groups;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Groups
{
	public class DeleteGroupHandler : ICommandHandler<DeleteGroupCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteGroupHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Group group = await unitOfwork.GroupRepository.GetByIdAsync(request.Id);
				if (group == null)
					return Result.Failure<int>(new Error(code: "Delete Group", message: "No Group has this Id")) ;

				//if (
				//	 (group.StudentHeadName != request.GroupDto.StudentHeadName) ||
				//	 (group.StudentHeadPhone != request.GroupDto.StudentHeadPhone) ||
				//	 (group.Name!=request.GroupDto.Name) 
				
				//	) 
				//{
				//	return Result.Failure<int>(new Error(code: "Delete Group", message: "Data of the Group is not the same in database"));
				//}

				bool IsDeleted = await unitOfwork.GroupRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Group", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Group", message: ex.Message.ToString()));
			}
		}
	}
}
