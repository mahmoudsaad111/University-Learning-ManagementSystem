using Application.Common.Interfaces.FileProcessing;
using Contract.Dto.Files;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileProcessing
{
    public class FileCourseProcessingService : IFileCourseProcessing
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private string WebRootPath = string.Empty;
        public FileCourseProcessingService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            WebRootPath = webHostEnvironment.WebRootPath;
        }
        public string GetFileForCoursePathInsidewwwroot (int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int LectureId)
        {
            return FacultyId + "\\" + DepartementId + "\\" + AcadimicYearId + "\\" + "Courses" + "\\" + CourseCycleId + "\\" + LectureId;
        
        }
        public string GetFileForCoursePath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int LectureId)
        {
            return WebRootPath + "\\" + GetFileForCoursePathInsidewwwroot(FacultyId,DepartementId, AcadimicYearId, CourseCycleId, LectureId);
        }

        public async Task<Result<string>> UploadFileForCourse(UploadFileToCourseDto uploadFileToCourseDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int LectureId)
        {
            try
            {
                string FolderPath = GetFileForCoursePath(FacultyId,DepartementId,AcadimicYearId,CourseCycleId,LectureId);

                if (!System.IO.File.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }
                string FileNameAndExtention = uploadFileToCourseDto.Name + "." + uploadFileToCourseDto.FileType.ToString().ToLower();
                
                string FilePath = FolderPath + "//" + FileNameAndExtention;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }

                using (FileStream stream = System.IO.File.Create(FilePath))
                {
                    await uploadFileToCourseDto.FormFile.CopyToAsync(stream);
                    string RetuenedUrl = GetFileForCoursePathInsidewwwroot(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, LectureId) + "/" + FileNameAndExtention;
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
