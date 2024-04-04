using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Sections
{
	public class UpdateDepartementHandler : ICommandHandler<UpdateSectionCommand, Section>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateDepartementHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Section>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Section section = unitOfwork.SectionRepository.Find(f => f.SectionId == request.Id);
				if (section is null)
					return Result.Failure<Section>(new Error(code: "Update Section", message: "No Section exist by this Id"));
				
				section.Name = request.SectionDto.Name;
				section.CourseCycleId = request.SectionDto.CourseCycleId;				 
				section.InstructorId = request.SectionDto.InstructorId;
				section.Description = request.SectionDto.Description;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Section>(section);
			}
			catch (Exception ex)
			{
				return Result.Failure<Section>(new Error(code: "Updated Section" , message: ex.Message.ToString())); 
			}
		}
	}
}
