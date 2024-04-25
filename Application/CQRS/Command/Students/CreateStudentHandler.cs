using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.StudentCourseCycles;
using Application.CQRS.Query.CourseCycles;
using Contract.Dto.StudentCourseCycles;
using Contract.Dto.UsersRegisterDtos;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Students
{
	public class CreateStudentHandler : ICommandHandler<CreateStudentCommand, StudentRegisterDto>
	{
		private readonly IUnitOfwork unitOfwork;
		private IMediator mediator;
        public CreateStudentHandler(IUnitOfwork unitOfwork, IMediator mediator)
        {
            this.unitOfwork = unitOfwork;
            this.mediator = mediator;
        }
        public async Task<Result<StudentRegisterDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = request.StudentRegisterDto.GetStudent();
				var Student = await unitOfwork.StudentRepository.CreateAsync(request.StudentRegisterDto.GetStudent());

				if(Student is  null)
				{

					return Result.Failure<StudentRegisterDto>(new Error(code: "Create Student", message: "Can not Create")) ;
                }


				bool Done =await unitOfwork.StudentCourseCycleRepository.AddStudentToHisCourseCycles
					                                                     (studentId: Student.StudentId,AcadimicYearId: Student.AcadimicYearId,GroupId: Student.GroupId);

				if (Done)
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Create<StudentRegisterDto>(request.StudentRegisterDto);
				}


                return Result.Failure<StudentRegisterDto>(new Error(code: "Create Student", message:"Can not Add student to his courses groups")); 

            }
            catch (Exception ex)
			{
				return  Result.Failure<StudentRegisterDto>(new Error (code :"Create Student",message: ex.Message.ToString() ));
			}
		}
	}
}
