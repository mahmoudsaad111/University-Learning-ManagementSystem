using Domain.Models;
using Domain.TmpFilesProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.FileResources
{
    public class FileResourceDto : FileModel
    {

        // The two commented prop s are in FileModel
        //   public string Name { get; set; }
        // public FileType FileType { get; set; }

        public string Url { get; set; }
        public int LectureId { get; set; }
        public int AssignmentId {  get; set; }
        public int AssignmentAnswerId { get; set; } 

        public LectureResource GetLectureResource()
        {
            return new LectureResource
            {
                Name = Name,
                Url = Url,
                FileType = FileType,
                LectureId = LectureId
            };
        }

        public AssignmentResource GetAssignmentResource()
        {
            return new AssignmentResource
            {
                Name = Name,
                Url = Url,
                FileType = FileType,
                AssignmentId = AssignmentId
            };
        }

        public AssignmentAnswerResource GetAssignmentAnswerResource()
        {
            return new AssignmentAnswerResource
            {
                Name = Name,
                Url = Url,
                FileType = FileType,
                AssignmentAnswerId = AssignmentAnswerId
            };
        }
    }
}
