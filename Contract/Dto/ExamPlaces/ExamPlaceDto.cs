using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.ExamPlaces
{
    public class ExamPlaceDto
    {
        public ExamType ExamType { get; set; }
        public int CourseId { get; set; }   
        public int SectionId {  get; set; } 
        public int CourseCycleId { get; set; }

        public ExamPlace GetExamPlaceOfCourseSemester()
        {
            return new ExamPlace
            {
                CourseId = CourseId,
                ExamType = ExamType.Semester
            };
        }

        public ExamPlace GetExamPlaceOfCourseFinal()
        {
            return new ExamPlace
            {
                CourseId = CourseId,
                ExamType = ExamType.Final
            };
        }

        public ExamPlace GetExamPlaceOfCourseCycleQuiz()
        {
            return new ExamPlace
            {
                CourseCycleId = CourseCycleId,
                ExamType = ExamType.Quiz
            };
        }
        public ExamPlace GetExamPlaceOfCourseCycleMidterm()
        {
            return new ExamPlace
            {
                CourseCycleId = CourseCycleId,
                ExamType = ExamType.Midterm
            };
        }

        public ExamPlace GetExamPlaceOfSectioneQuiz()
        {
            return new ExamPlace
            {
                SectionId = SectionId,
                ExamType = ExamType.Quiz
            };
        }
    }
}
