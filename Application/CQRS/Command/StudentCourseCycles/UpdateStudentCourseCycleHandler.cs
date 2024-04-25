using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.StudentCourseCycles
{
    public class UpdateStudentCourseCycleHandler : ICommandHandler<UpdateStudentCourseCycleCommand, StudentCourseCycle>
    {
        private readonly IUnitOfwork unitOfwork;

        public UpdateStudentCourseCycleHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<StudentCourseCycle>> Handle(UpdateStudentCourseCycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StudentCourseCycle newStudentCourseCycle =await unitOfwork.StudentCourseCycleRepository.GetByIdAsync(request.Id);

                if (newStudentCourseCycle == null)
                {
                    return Result.Failure<StudentCourseCycle>(new Error(code: "UpdateStudentCourseCycle", message: "No item by this Id")) ;
                }

                newStudentCourseCycle.StudentId = request.StudentCourseCycleDto.StudentId;
                newStudentCourseCycle.CourseCycleId = request.StudentCourseCycleDto.CourseCycleId;


                // note that i do not SaveChanges here because this operation will invoke from only "AddStudent"
                // so if the result here is success do SaveChanges in AddStudent 

                return Result.Success<StudentCourseCycle>(newStudentCourseCycle);
            }
            catch (Exception ex) 
            {
                return Result.Failure<StudentCourseCycle>(new Error(code: "UpdateStudentCourseCycle", message: ex.Message.ToString())) ;

            }

        }
    }
}
