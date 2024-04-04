using Application.Common.Interfaces.CQRSInterfaces;
using Application.enums;
using Contract.Dto.FileResources;
using Domain.Models;
using Domain.TmpFilesProcessing;


namespace Application.CQRS.Command.FileResources
{

    // this class to create 

    public class CreateFileResourceCommand :ICommand<FileResource>
    {
        public FileResourceDto FileResourceDto { get; set; }    
        public EntitiesHasFiles TypeOfEntity { get; set; }  
    }
}
