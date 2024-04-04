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
    public class DeleteCourseCategoryHandler : ICommandHandler<DeleteCourseCategoryCommand , int>
    {

        private readonly IUnitOfwork unitOfwork;

        public DeleteCourseCategoryHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(DeleteCourseCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CourseCategory courseCategory = await unitOfwork.CourseCategoryRepository.GetByIdAsync(request.Id);
                if (courseCategory == null)
                    Result.Failure<int>(new Error(code: "Delete CourseCategory", message: "No courseCategory has this Id"));

                if ((courseCategory.DepartementId != request.CourseCategoryDto.DepartementId) ||
                     (courseCategory.Name != request.CourseCategoryDto.Name) ||
                     (courseCategory.Description != request.CourseCategoryDto.Description)
                    )
                {
                    return Result.Failure<int>(new Error(code: "Delete CourseCategory", message: "Data of the courseCategory is not the same in database"));
                }

                bool IsDeleted = await unitOfwork.CourseCategoryRepository.DeleteAsync(request.Id);

                if (IsDeleted)
                {
                    int NumOfTasks = await unitOfwork.SaveChangesAsync();
                    return Result.Success<int>(request.Id);
                }
                return Result.Failure<int>(new Error(code: "Delete CourseCategory", message: "Unable To Delete"));
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "Delete CourseCategory", message: ex.Message.ToString()));
            }
        }
    }
}
