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
using Domain.Enums;
using Contract.Dto.ExamPlaces;
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

                await unitOfwork.SaveChangesAsync(); // to Ensure that Course get identityId from database to pass it to ExamPlcaeDto
             
                ExamPlaceDto examPlaceDto = new ExamPlaceDto
                {
                    CourseId = course.CourseId
                };

                ExamPlace ExamPlaceFinalExamOfThisCourse = await unitOfwork.ExamPlaceRepository.CreateAsync(examPlaceDto.GetExamPlaceOfCourseFinal());

                ExamPlace ExamPlaceSemesterOfThisCourse = await unitOfwork.ExamPlaceRepository.CreateAsync(examPlaceDto.GetExamPlaceOfCourseSemester());


                if (ExamPlaceFinalExamOfThisCourse is null || ExamPlaceSemesterOfThisCourse is null)
                {
                     unitOfwork.CourseRepository.DeleteAsync(course.CourseId);
                     await unitOfwork.SaveChangesAsync(); 

                    return Result.Failure<Course>(new Error(code: "CreateCourseHandler", message: "Unable to add Exam Places of the course"));
                }

                
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
