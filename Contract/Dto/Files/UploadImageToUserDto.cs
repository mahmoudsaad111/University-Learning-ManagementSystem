 
using Domain.TmpFilesProcessing;
using Microsoft.AspNetCore.Http;
using Domain.Enums; 

namespace Contract.Dto.Files
{
    public class UploadImageToUserDto :FileModel
    {
        public int FacultyId { get; set; }
        public int DepartementId {  get; set; } 
        public string UserName { get; set; }    
        public IFormFile FormFile { get; set; }
        public TypesOfUsers TypeOfUser { get; set; }
    }
}
