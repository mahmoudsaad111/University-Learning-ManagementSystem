using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.FilesProcessing
{
    public class UploadImageToUserCommand :ICommand<string>
    {
        public UploadImageToUserDto UploadImageToUserDto { get; set; }  
    }
}
