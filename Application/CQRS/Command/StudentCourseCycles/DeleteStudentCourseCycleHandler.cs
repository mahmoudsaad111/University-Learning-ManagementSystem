using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.StudentCourseCycles
{
    public class DeleteStudentCourseCycleHandler : ICommandHandler<DeleteStudentCourseCycleCommand, int>
    {
        private readonly IUnitOfwork unitOfwork; 

        public async Task<Result<int>> Handle(DeleteStudentCourseCycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StudentCourseCycle studentCourseCycle = await unitOfwork.StudentCourseCycleRepository.GetByIdAsync(request.Id);
                if (studentCourseCycle == null)
                {
                    return Result.Failure<int>(new Error(code: "DeleteStudentCourseCycle", message: "NullRefrence"));
                }

                bool IsDeleted = await unitOfwork.StudentCourseCycleRepository.DeleteAsync(request.Id);

                if (IsDeleted)
                {
                    // note that i do not SaveChanges here because this operation will invoke from only "AddStudent"
                    // so if the result here is success do SaveChanges in AddStudent 
                    // do not forget to save in student
                    return Result.Success<int>(request.Id);
                }
                return Result.Failure<int>(new Error(code: "DeleteStudentCourseCycle", message: "NullRefrence"));

            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "DeleteStudentCourseCycle", message: ex.Message.ToString())); 
            }
        }
    }
}
