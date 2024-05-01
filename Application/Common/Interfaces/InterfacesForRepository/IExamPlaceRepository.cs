using Application.Common.Interfaces.Presistance;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IExamPlaceRepository :IBaseRepository<ExamPlace>
    {
        //public ExamPlace AddExamPlaceToSection(int  sectionId );;
        //public ExamPlace AddExamPlaceToCourseCycle(int courseCylceId);

        //public ExamPlace AddExamPlaceToCourse (int courseId, ExamType examType);

    }
}
