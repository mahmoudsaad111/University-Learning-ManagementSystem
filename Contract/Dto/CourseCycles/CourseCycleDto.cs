

using Domain.Models;

namespace Contract.Dto.CourseCycles
{
    public class CourseCycleDto
    {
        public string Title { get; set; }
        public int GroupId { get; set; }      
        public int CourseId { get; set; }
        public int ProfessorId { get; set; }

        public CourseCycle GetCourseCycle()
        {
            return new CourseCycle
            {
                CourseId = (CourseId == 0) ? 1 : CourseId,
                Title = Title,
                GroupId = (GroupId == 0) ? 1 : GroupId,
                ProfessorId = (ProfessorId == 0) ? 1 : ProfessorId
            };
        }
      
    }
}
