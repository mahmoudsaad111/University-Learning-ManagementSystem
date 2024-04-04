using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Sections
{
	public class DeleteSectionHandler : ICommandHandler<DeleteSectionCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteSectionHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Section section = await unitOfwork.SectionRepository.GetByIdAsync(request.Id);
				if (section == null)
					return Result.Failure<int>(new Error(code: "Delete Section", message: "No section has this Id")) ;

				if ((section.CourseCycleId != request.SectionDto.CourseCycleId) ||
					 (section.Description != request.SectionDto.Description) ||
					 (section.InstructorId != request.SectionDto.InstructorId) ||
					 (section.Name!=request.SectionDto.Name) 
					 
					) 
				{
					return Result.Failure<int>(new Error(code: "Delete Section", message: "Data of the section is not the same in database"));
				}

				bool IsDeleted = await unitOfwork.SectionRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Section", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Section", message: ex.Message.ToString()));
			}
		}
	}
}
