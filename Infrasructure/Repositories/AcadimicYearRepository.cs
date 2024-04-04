using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Models;
using Domain.Shared;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class AcadimicYearRepository : BaseRepository<AcadimicYear> , IAcadimicYearRepository
    {
        public AcadimicYearRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<AcadimicYear> GetAcadimicYearHasSpecificCourseIdAndGroupAsync(int courseId, int groupId)
        {
            try
            {
                var acadimicYear = await _appDbContext.AcadimicYears.
                                                                AsNoTracking().
                                                                //Include(ay=>ay.Departement).
                                                                FirstOrDefaultAsync(ay => ay.Courses.Any(c => c.CourseId == courseId) && 
                                                                                    ay.Groups.Any(g => g.GroupId == groupId));
                return acadimicYear;
            }
            catch (Exception ex)
            {
                return null; 
            }            
        }

        public async Task<Result<int>> GetAcadimicYearIdByDepIdAndYearNum(int depId , int yearNum)
        {
            var AcYear= await _appDbContext.AcadimicYears.AsNoTracking().FirstOrDefaultAsync(d => d.Year == yearNum && d.DepartementId == depId);
            if (AcYear is null)
                return Result.Failure<int>(new Error(code: "FindAcadimicYearId", message: "No Acadimic year"));
            return Result.Success<int>(AcYear.AcadimicYearId);
        }
    }
}
