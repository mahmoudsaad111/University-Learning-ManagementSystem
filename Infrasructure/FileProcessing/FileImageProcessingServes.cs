using Application.Common.Interfaces.FileProcessing;
using Contract.Dto.Files;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileProcessing
{
    public class FileImageProcessingServes : IFileImageProcessing
    {

        protected TypesOfUsers TypeOfUser = TypesOfUsers.Student;
        private readonly IWebHostEnvironment webHostEnvironment;
        private string WebRootPath = string.Empty;
        public FileImageProcessingServes( IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            WebRootPath = webHostEnvironment.WebRootPath;
           // TypeOfUser = typeOfUser;
        }
        public void SetTypeOfUser(TypesOfUsers typeOfUser)
        {
            this.TypeOfUser=typeOfUser;
        }

        public string GetFileForImagePath(int FacultyId, int DepartementId)
        {
           return WebRootPath + "\\" + GetFileForImagePathInsidewwwroot(FacultyId , DepartementId); 
        }

        public string GetFileForImagePathInsidewwwroot(int FacultyId, int DepartementId)
        {
            return FacultyId + "\\" + DepartementId + "\\" + TypeOfUser.ToString() ;
        }

        public async Task<Result<string>> UploadFileImageToUser(UploadImageToUserDto UploadImageToUserDto)
        {
            try
            {
                string FolderPath = GetFileForImagePath(UploadImageToUserDto.FacultyId, UploadImageToUserDto.DepartementId);

                if (!System.IO.File.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }
                string FileNameAndExtention = UploadImageToUserDto.Name + "." + UploadImageToUserDto.FileType.ToString().ToLower();

                string FilePath = FolderPath + "//" + FileNameAndExtention;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }

                using (FileStream stream = System.IO.File.Create(FilePath))
                {
                    await UploadImageToUserDto.FormFile.CopyToAsync(stream);
                    string RetuenedUrl = GetFileForImagePathInsidewwwroot(UploadImageToUserDto.FacultyId, UploadImageToUserDto.DepartementId) + "/" + FileNameAndExtention;
                    return Result.Success<string>(RetuenedUrl);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("", ""));
            }
        }

 
    }
}
