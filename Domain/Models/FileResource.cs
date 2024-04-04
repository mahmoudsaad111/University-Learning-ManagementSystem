using Domain.TmpFilesProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FileResource   :  FileModel
    {
        public int FileResourceId { get; set; }
        public string Url { get; set; }

        // the Two commented proprties are in File Model 
        //    public string Name { get; set; } = null!;
        //     public FileType FileType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
