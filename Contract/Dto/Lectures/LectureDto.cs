using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Lectures
{
    public class LectureDto
    {

        public string Name { get; set; } = null!;
        public bool HavingAssignment { get; set; }
        public int? SectionId { get; set; }
        public int? CourseCycleId { get; set; }
        public string VideoUrl { get; set; }

        public Lecture GetLecture()
        {
            if ((CourseCycleId == 0 && SectionId == 0) ||
                  (CourseCycleId != 0 && SectionId != 0)
                 )
                return null;


            if(CourseCycleId!=0)
                return new Lecture
                    {
                        Name = Name,
                        HavingAssignment = HavingAssignment,         
                        CourseCycleId = CourseCycleId , 
                        VedioUrl = VideoUrl 
                    };

            // this lecture belongs to Section not CourseCycle
            return new Lecture
            {
                Name = Name,
                HavingAssignment = HavingAssignment,
                SectionId = SectionId,
                VedioUrl= VideoUrl
            };
        }
    }
}
