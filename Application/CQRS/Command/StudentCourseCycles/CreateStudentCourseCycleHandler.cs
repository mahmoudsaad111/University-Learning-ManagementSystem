using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.StudentCourseCycles
{
    public class CreateStudentCourseCycleHandler : ICommandHandler<CreateStudentCourseCycleCommand, StudentCourseCycle>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateStudentCourseCycleHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<StudentCourseCycle>> Handle(CreateStudentCourseCycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StudentCourseCycle studentCourseCycle =await unitOfwork.StudentCourseCycleRepository.CreateAsync(request.StudentCourseCycleDto.GetStudentCourseCycle());

                // note that i do not SaveChanges here because this operation will invoke from only "AddStudent"
                // so if the result here is success do SaveChanges in AddStudent 
                return Result.Success<StudentCourseCycle> (studentCourseCycle);
            }
            catch(Exception ex)
            {
                return Result.Failure<StudentCourseCycle>(new Error(code: "CreateStudentCourseCycle", message: ex.Message.ToString()));
            }
        }
    }
}
