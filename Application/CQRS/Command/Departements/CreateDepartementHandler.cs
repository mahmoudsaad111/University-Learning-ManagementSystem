using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Departements
{
	public class CreateDepartementHandler : ICommandHandler<CreateDepartementCommand ,Departement>
	{
		private readonly IUnitOfwork unitOfwork;
		public CreateDepartementHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<Departement>> Handle(CreateDepartementCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Departement createdDepartement = await unitOfwork.DepartementRepository.CreateAsync(request.DepartementDto.GetDepartement());
				if (createdDepartement is null)
					return Result.Failure<Departement>(new Error(code: "Added departement", message: "Unable to add departement"));

				await unitOfwork.SaveChangesAsync();
				return Result<Departement>.Success(createdDepartement);
			}
			catch (Exception ex)
			{
				return Result.Failure<Departement>(new Error("can't create new departement", ex.Message.ToString()));
			}
		}
	}
}
