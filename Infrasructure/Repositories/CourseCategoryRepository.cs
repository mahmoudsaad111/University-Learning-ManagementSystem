using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.CourseCategories;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class CourseCategoryRepository : BaseRepository<CourseCategory> , ICourseCategoryRepository
	{
		public CourseCategoryRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

        public async Task<IEnumerable<CourseCategoryWithDeptAndFacultyDto>> GetAllCourseCategoriesWithDeptAndFaculty()
        {
            var CourseCategoriesWithDeptAndFac = from CC in _appDbContext.CourseCategories
                                           join Dept in _appDbContext.Departements on CC.DepartementId equals Dept.DepartementId
                                           join Fac in _appDbContext.Faculties on Dept.FacultyId equals Fac.FacultyId
                                           select new CourseCategoryWithDeptAndFacultyDto
                                           {
                                               CourseCategoryId = CC.CourseCategoryId,
                                               CourseCategoryName = CC.Name,
                                               CourseCategoryDescription = CC.Description,
                                               DepartementId = CC.DepartementId,
                                               DepartementName = Dept.Name,
                                               FacultyId = Fac.FacultyId,
                                               FacultyName = Fac.Name
                                           };

                                                     
            return CourseCategoriesWithDeptAndFac;           
        }
    }
}
