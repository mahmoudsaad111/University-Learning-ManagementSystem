using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IAcadimicYearRepository :IBaseRepository<AcadimicYear>
    {
        public Task<Result<int>> GetAcadimicYearIdByDepIdAndYearNum(int depId, int yearNum);
        public Task<AcadimicYear> GetAcadimicYearHasSpecificCourseIdAndGroupAsync(int courseId, int groupId);
      //  public Task<AcadimicYear> GetAcadimicYearHasSpecificCourse(int courseId);
       
    }
}
