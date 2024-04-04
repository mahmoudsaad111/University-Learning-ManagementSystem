using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Faculties;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.CourseCategories
{
    public class UpdateCourseCategoryHandler :ICommandHandler<UpdateCourseCategoryCommand , CourseCategory>
    {
        private readonly IUnitOfwork unitOfwork;
        public UpdateCourseCategoryHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<CourseCategory>> Handle(UpdateCourseCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CourseCategory courseCategory = unitOfwork.CourseCategoryRepository.Find(f => f.CourseCategoryId == request.Id);
                if (courseCategory is null)
                    return Result.Failure<CourseCategory>(new Error(code: "Update CourseCategory", message: "No CourseCategory exist by this Id"));

                courseCategory.Description = request.CourseCategoryDto.Description;
                courseCategory.DepartementId = request.CourseCategoryDto.DepartementId;
                courseCategory.Name = request.CourseCategoryDto.Name;
               
                int NumOfTasks = await unitOfwork.SaveChangesAsync();
                return Result.Success<CourseCategory>(courseCategory);
            }
            catch (Exception ex)
            {
                return Result.Failure<CourseCategory>(new Error(code: "Updated CourseCategory", message: ex.Message.ToString()));
            }
        }
    }
}
