using Application.Common.Interfaces.FileProcessing;
using Contract.Dto.Files;
using Domain.Models;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Infrastructure.FileProcessing
{
    public class FileAssignementProcessingService : IFileAssignementProcessing
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private string WebRootPath = string.Empty;
        public FileAssignementProcessingService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            WebRootPath = webHostEnvironment.WebRootPath;
        }
        public string GetFileForAssignementPath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId)
        {
            return WebRootPath + "\\" + GetFileForAssignementPathInsidewwwroot(FacultyId,DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignemenetId);

        }

        public string GetFileForAssignementPathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId)
        {
            return FacultyId + "\\" + DepartementId + "\\" + AcadimicYearId + "\\" + "Assignements" + "\\" + CourseCycleId + "\\" + SectionId + "\\" + AssignemenetId ;

        }

        public async Task<Result<string>> UploadFileForAssignement(UploadFileToAssignementDto uploadFileToAssignementDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId)
        {
            try
            {
                string FolderPath = GetFileForAssignementPath(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId,AssignemenetId);
                if (!System.IO.File.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }
                string FileNameAndExtention = uploadFileToAssignementDto.Name + "." + uploadFileToAssignementDto.FileType.ToString().ToLower();

                string FilePath = FolderPath + "//" + FileNameAndExtention;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                using (FileStream stream = System.IO.File.Create(FilePath))
                {
                    await uploadFileToAssignementDto.FormFile.CopyToAsync(stream);
                    string RetuenedUrl = GetFileForAssignementPathInsidewwwroot(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignemenetId) + "/" + FileNameAndExtention;
                    return Result.Success<string>(RetuenedUrl);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileForAssignement", "Exeption"));

            }
        }
    }
}
