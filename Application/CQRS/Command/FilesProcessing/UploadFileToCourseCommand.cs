using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.FilesProcessing
{
    public class UploadFileToCourseCommand :ICommand<string>
    {
        public UploadFileToCourseDto uploadFileToCourseDto {  get; set; }    
    }
}
