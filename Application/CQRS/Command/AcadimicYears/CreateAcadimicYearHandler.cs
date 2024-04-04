using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.AcadimicYears
{
	public class CreateAcadimicYearHandler : ICommandHandler<CreateAcadimicYearCommand, AcadimicYear>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateAcadimicYearHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<AcadimicYear>> Handle(CreateAcadimicYearCommand request, CancellationToken cancellationToken)
		{
			try
			{
				AcadimicYear AcadimicYear =await unitOfwork.AcadimicYearRepository.CreateAsync(request.AcadimicYearDto.GetAcadimicYear()) ;
				if (AcadimicYear is null)
					return Result.Failure<AcadimicYear>(new Error(code: "Create AcadimicYear" ,message :"not valid data" ));

				await unitOfwork.SaveChangesAsync();
				return Result.Success<AcadimicYear>(AcadimicYear);
			}
			catch(Exception ex) 
			{
				return Result.Failure<AcadimicYear>(new Error(code: "Create AcadimicYear", message: ex.Message.ToString())) ;
			}
		}
	}
}
