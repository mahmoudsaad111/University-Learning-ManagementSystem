using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.UsersRegisterDtos;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Students
{
	public class CreateStudentHandler : ICommandHandler<CreateStudentCommand, StudentRegisterDto>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateStudentHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<StudentRegisterDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = request.StudentRegisterDto.GetStudent();
				await unitOfwork.StudentRepository.CreateAsync(request.StudentRegisterDto.GetStudent());
				int NumOfTasks = await unitOfwork.SaveChangesAsync();
				return Result.Create<StudentRegisterDto>(request.StudentRegisterDto);
			}
			catch (Exception ex)
			{
				return  Result.Failure<StudentRegisterDto>(new Error (code :"Create Student",message: ex.Message.ToString() ));
			}
		}
	}
}
