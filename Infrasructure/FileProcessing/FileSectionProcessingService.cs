using Application.Common.Interfaces.FileProcessing;
using Contract.Dto.Files;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileProcessing
{
    public class FileSectionProcessingService : IFileSectionProcessing
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private string WebRootPath = string.Empty;
        public FileSectionProcessingService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            WebRootPath = webHostEnvironment.WebRootPath;
        }
        public string GetFileForSectionPathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int LectureId)
        {
            return FacultyId + "\\" + DepartementId + "\\" + AcadimicYearId + "\\" + "Sections" + "\\" + CourseCycleId + "\\" +SectionId +"\\"+LectureId;

        }
        public string GetFileForSectionPath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int LectureId)
        {
            return WebRootPath + "\\" + GetFileForSectionPathInsidewwwroot(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, LectureId);
        }

        public async Task<Result<string>> UploadFileForSection(UploadFileToSectionDto uploadFileToSectionDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int LectureId)
        {
            try
            {
                string FolderPath = GetFileForSectionPath(FacultyId, DepartementId, AcadimicYearId, CourseCycleId,SectionId, LectureId);

                if (!System.IO.File.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }
                string FileNameAndExtention = uploadFileToSectionDto.Name + "." + uploadFileToSectionDto.FileType.ToString().ToLower();

                string FilePath = FolderPath + "//" + FileNameAndExtention;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }

                using (FileStream stream = System.IO.File.Create(FilePath))
                {
                    await uploadFileToSectionDto.FormFile.CopyToAsync(stream);
                    string RetuenedUrl = GetFileForSectionPathInsidewwwroot(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, LectureId) + "/" + FileNameAndExtention;
                    return Result.Success<string>(RetuenedUrl);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileForSection", "Exeption"));
            }
        }
    }
}
