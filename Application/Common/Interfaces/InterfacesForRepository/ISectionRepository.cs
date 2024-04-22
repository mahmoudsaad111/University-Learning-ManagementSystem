using Application.Common.Interfaces.Presistance;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ISectionRepository :IBaseRepository<Section>   
    {
        public Task<Section> GetSectionHasSpecificLectureUsingLectureIdAsync(int LectureId);
        public Task<IEnumerable<int>> GetAllSectionsIdOfInstructore(int instructorId);
        public Task<IEnumerable<int>> GetAllSectionsIdOfProfessor(int ProfesssorId);
    }
}
