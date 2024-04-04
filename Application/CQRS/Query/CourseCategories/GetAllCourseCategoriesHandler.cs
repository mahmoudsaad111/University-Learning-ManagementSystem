using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;

namespace Application.CQRS.Query.CourseCategories
{
    public class GetAllCourseCategoriesHandler : IQueryHandler<GetAllCourseCategoriesQuery, IEnumerable<CourseCategory>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllCourseCategoriesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<CourseCategory>>> Handle(GetAllCourseCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courseCategories = await unitOfwork.CourseCategoryRepository.FindAllAsyncInclude();
                return Result.Create<IEnumerable<CourseCategory>>(courseCategories);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<CourseCategory>>(new Error(code: "GetAllCourseCategories", message: ex.Message.ToString()));
            }
        }
    }
}
