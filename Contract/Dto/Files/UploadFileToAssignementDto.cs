using Domain.TmpFilesProcessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Files
{
    public class UploadFileToAssignementDto : FileModel
    {
        public int SectionId {  get; set; }
        public int AssignementId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
