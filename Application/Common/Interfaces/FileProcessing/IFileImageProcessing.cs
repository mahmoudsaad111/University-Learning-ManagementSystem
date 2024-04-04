using Contract.Dto.Files;
using Domain.Enums;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.FileProcessing
{
    public interface IFileImageProcessing
    {
        public void SetTypeOfUser(TypesOfUsers typeOfUser);
        public string GetFileForImagePathInsidewwwroot(int FacultyId, int DepartementId);
        public string GetFileForImagePath(int FacultyId, int DepartementId );
        public Task<Result<string>> UploadFileImageToUser(UploadImageToUserDto UploadImageToUserDto  );
    }
}
