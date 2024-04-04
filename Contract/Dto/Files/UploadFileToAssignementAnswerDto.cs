using Domain.TmpFilesProcessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Files
{
    public class UploadFileToAssignementAnswerDto : FileModel
    {
        public int AssignementId { get; set; }
        public int AssignementAnswerId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
