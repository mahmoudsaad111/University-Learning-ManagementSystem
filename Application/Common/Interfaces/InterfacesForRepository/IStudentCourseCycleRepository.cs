using Application.Common.Interfaces.Presistance;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IStudentCourseCycleRepository :IBaseRepository<StudentCourseCycle>
    {

        public Task<bool> AddStudentToHisCourseCycles(int studentId, int AcadimicYearId, int GroupId);
        public Task<bool> DeleteStudentFromHisCourseCylces(int studentId);

        public Task<int> GetStudentCourseCycleId(int studentId, int courseCycleId);
 
        //public Task

    }
}
