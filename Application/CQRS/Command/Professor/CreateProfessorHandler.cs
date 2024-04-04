using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.UsersRegisterDtos;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Professors
{
	public class CreateProfessorHandler : ICommandHandler<CreateProfessorCommand, ProfessorRegisterDto>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateProfessorHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<ProfessorRegisterDto>> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
		{
			try
			{
				await unitOfwork.ProfessorRepository.CreateAsync(request.ProfessorRegisterDto.GetProfessor());
				int NumOfTasks = await unitOfwork.SaveChangesAsync();
				return Result.Create<ProfessorRegisterDto>(request.ProfessorRegisterDto);
			}
			catch (Exception ex)
			{
				return  Result.Failure<ProfessorRegisterDto>(new Error (code :"Create Professor",message: ex.Message.ToString() ));
			}
		}
	}
}
