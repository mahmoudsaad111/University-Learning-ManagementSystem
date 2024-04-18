using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Courses;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Courses
{
    public class CreateCourseHandler : ICommandHandler<CreateCourseCommand, Course>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreateCourseHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<Course>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Course course = await unitOfwork.CourseRepository.CreateAsync(request.CourseDto.GetCourse());
                if (course is null)
                    return Result.Failure<Course>(new Error(code: "Create Course", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<Course>(course);
            }
            catch (Exception ex)
            {
                return Result.Failure<Course>(new Error(code: "Create Course", message: ex.Message.ToString()));
            }
        }
    }
}
