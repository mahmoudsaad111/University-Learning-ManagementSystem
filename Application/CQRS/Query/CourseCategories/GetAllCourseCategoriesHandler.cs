using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.CourseCategories;
using Domain.Models;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.CQRS.Query.CourseCategories
{
    public class GetAllCourseCategoriesHandler : IQueryHandler<GetAllCourseCategoriesQuery, IEnumerable<CourseCategoryWithDeptAndFacultyDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllCourseCategoriesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<CourseCategoryWithDeptAndFacultyDto>>> Handle(GetAllCourseCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courseCategories = await unitOfwork.CourseCategoryRepository.GetAllCourseCategoriesWithDeptAndFaculty();
                return Result.Create<IEnumerable<CourseCategoryWithDeptAndFacultyDto>>(courseCategories);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<CourseCategoryWithDeptAndFacultyDto>>(new Error(code: "GetAllCourseCategories", message: ex.Message.ToString()));
            }
        }
    }
}

