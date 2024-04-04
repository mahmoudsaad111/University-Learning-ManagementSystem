using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.AcadimicYears;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.AcadimicYears
{
	public class DeleteAcadimicYearHandler : ICommandHandler<DeleteAcadimicYearCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteAcadimicYearHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteAcadimicYearCommand request, CancellationToken cancellationToken)
		{
			try
			{
				AcadimicYear AcadimicYear = await unitOfwork.AcadimicYearRepository.GetByIdAsync(request.Id);
				if (AcadimicYear == null)
					return Result.Failure<int>(new Error(code: "Delete AcadimicYear", message: "No AcadimicYear has this Id")) ;

				if ((AcadimicYear.DepartementId != request.AcadimicYearDto.DepartementId) ||					 
					 (AcadimicYear.Year!=request.AcadimicYearDto.Year)
					) 
				{
					return Result.Failure<int>(new Error(code: "Delete AcadimicYear", message: "Data of the AcadimicYear is not the same in database"));
				}

				bool IsDeleted = await unitOfwork.AcadimicYearRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete AcadimicYear", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete AcadimicYear", message: ex.Message.ToString()));
			}
		}
	}
}
