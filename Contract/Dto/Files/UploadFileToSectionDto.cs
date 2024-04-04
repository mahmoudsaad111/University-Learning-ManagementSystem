using Domain.TmpFilesProcessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Files
{
    public class UploadFileToSectionDto : FileModel
    {
        public int LectureId { get; set; }
        public int SectionId { get ; set; }
        public IFormFile FormFile { get; set; }
    }
}
