using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
 
namespace Infrastructure.Repositories
{
	public class CourseCategoryRepository : BaseRepository<CourseCategory>
	{
		public CourseCategoryRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}
	}
}
