using Application.Common.Interfaces.Presistance;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ILectureRepository :IBaseRepository<Lecture>
    {
        public Task<IEnumerable<Comment>> GetLectureComments(int LectureId);
        public Task<IEnumerable<Lecture>> GetLecturesOfCourseCycle(int CourseCycleId);
        public Task<IEnumerable<Lecture>> GetLecturesOfSection (int SectionId); 
    }
}
