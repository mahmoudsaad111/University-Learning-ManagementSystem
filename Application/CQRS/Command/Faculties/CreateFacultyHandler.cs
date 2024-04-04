using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Faculties
{
	public class CreateFacultyHandler : ICommandHandler<CreateFacultyCommand ,Faculty>
	{
		private readonly IUnitOfwork unitOfwork;
		public CreateFacultyHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<Faculty>> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Faculty createdFaculty = await unitOfwork.FacultyRepository.CreateAsync(request.FacultyDto.GetFaculty());
				if (createdFaculty is null)
					return Result.Failure<Faculty>(new Error(code: "Added faculty", message: "Unable to add faculty"));

				await unitOfwork.SaveChangesAsync();
				return Result<Faculty>.Success(createdFaculty);
			}
			catch (Exception ex)
			{
				return Result.Failure<Faculty>(new Error("can't create new faculty", ex.Message.ToString()));
			}
		}
	}
}
