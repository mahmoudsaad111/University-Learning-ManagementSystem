using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.UsersRegisterDtos;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Instructors
{
	public class CreateInstructorHandler : ICommandHandler<CreateInstructorCommand, InstructorRegisterDto>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateInstructorHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<InstructorRegisterDto>> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
		{
			try
			{
				await unitOfwork.InstructorRepository.CreateAsync(request.InstructorRegisterDto.GetInstructor());
				int NumOfTasks = await unitOfwork.SaveChangesAsync();
				return Result.Create<InstructorRegisterDto>(request.InstructorRegisterDto);
			}
			catch (Exception ex)
			{
				return  Result.Failure<InstructorRegisterDto>(new Error (code :"Create Instructor",message: ex.Message.ToString() ));
			}
		}
	}
}
