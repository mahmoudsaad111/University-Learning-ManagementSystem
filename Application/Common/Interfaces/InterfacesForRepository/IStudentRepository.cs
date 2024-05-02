using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IStudentRepository :IBaseRepository<Student>
    {
        public Task<IEnumerable<ReturnedStudentDto>> GetAllStudentsInDepartementAndAcadimicYear( int AcadimicYearId);
        public Task<int>GetStudentIdUsingUserName( string userName );
        public Task<bool> CheckIfStudentInGroup(int StudetId, int GroupId);
    }
}
