using Domain.TmpFilesProcessing;
using Microsoft.AspNetCore.Http;

namespace Contract.Dto.Files
{
    public class UploadFileToCourseDto : FileModel
    {
        public int GroupId {  get; set; }
        public int  CourseId { get; set; } 
        public int LectureId { get; set; }  
        public IFormFile FormFile { get; set; }
    }
}
