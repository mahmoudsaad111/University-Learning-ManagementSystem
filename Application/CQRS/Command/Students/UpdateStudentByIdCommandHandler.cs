using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Students
{
	public class UpdateProfessorByIdCommandHandler : ICommandHandler<UpdateStudentByIdCommand>
	{
		private readonly IUnitOfwork unitOfwork;

		public UpdateProfessorByIdCommandHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result> Handle(UpdateStudentByIdCommand request, CancellationToken cancellationToken)
		{
			try
			{
				request.NewStudentInfo.Id = request.Id;
				var OldInfoStudent = await unitOfwork.StudentRepository.GetByIdAsync(request.Id);
				if (OldInfoStudent is null)
				{
                    return Result.Failure(new Error("UpdatedStudentById", "Nullable"));
                }
				
			 
		        if(OldInfoStudent.GroupId != request.NewStudentInfo.GroupId || OldInfoStudent.AcadimicYearId != request.NewStudentInfo.AcadimicYearId) 
				{
					try
					{
						bool Delete = await unitOfwork.StudentCourseCycleRepository.DeleteStudentFromHisCourseCylces(request.Id);

						if (!Delete) throw new Exception();

						bool Added = await unitOfwork.StudentCourseCycleRepository.AddStudentToHisCourseCycles(studentId: request.Id,
																	 GroupId: request.NewStudentInfo.GroupId, AcadimicYearId: request.NewStudentInfo.AcadimicYearId) ;

						if (!Added) throw new Exception();
                      
                    }
					catch(Exception ex) 
					{
                        return Result.Failure(new Error("UpdatedStudentById", "Uanble to Update Student's Group of Courses "));
                    }
				}


				bool IsUpdated = await unitOfwork.StudentRepository.UpdateAsync(request.NewStudentInfo.GetStudent());
				if (IsUpdated)
				{
					//int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success();
				}
				return Result.Failure(new Error ("UpdatedStudentById" , "Uanble to Update Student"));
			}
			catch (Exception ex)
			{
				return Result.Failure(new Error(code: "UpdateStudent.Exception", message: ex.Message.ToString()));
			}
		}
	}
}
