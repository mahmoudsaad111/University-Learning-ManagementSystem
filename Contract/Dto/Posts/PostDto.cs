using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Posts
{
    public class PostDto
    {
    
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public bool GlobalToGroup { get; set; }
        public bool IsProfessor { get; set; }
        public int? SectionId { get; set; }
   
        public int? CourseCycleId { get; set; }
 
        public int PublisherId { get; set; }
        
        public Post GetPost()
        {
            if ((CourseCycleId == 0 && SectionId == 0) ||
                 (CourseCycleId != 0 && SectionId!=0)
                )
                 return null;
            if (CourseCycleId != 0)
                return new Post
                {
                    Content = Content,
                    CreatedBy = CreatedBy,
                    GlobalToGroup = GlobalToGroup,
                    CourseCycleId = CourseCycleId,
                    PublisherId = PublisherId,
                    IsProfessor = IsProfessor
                };

            // Post belong to section not to CourseCycle
            return new Post
            {
                Content = Content,
                CreatedBy = CreatedBy,
                GlobalToGroup = GlobalToGroup,
                SectionId=SectionId,
                PublisherId = PublisherId,
                IsProfessor = IsProfessor
            };
        }
    }
}
