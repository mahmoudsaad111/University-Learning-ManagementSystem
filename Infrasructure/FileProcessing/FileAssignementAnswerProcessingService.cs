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
    public class FileAssignementAnswerProcessingService : IFileAssignementAnswerProcessing
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private string WebRootPath = string.Empty;
        public FileAssignementAnswerProcessingService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            WebRootPath = webHostEnvironment.WebRootPath;
        }
        public string GetFileForAssignementAnswerPath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId, int AssignemenetAnswerId)
        {
            return WebRootPath + "\\" + GetFileForAssignementAnswerPathInsidewwwroot(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignemenetId,  AssignemenetAnswerId);

        }

        public string GetFileForAssignementAnswerPathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId, int AssignemenetAnswerId)
        {
            return FacultyId + "\\" + DepartementId + "\\" + AcadimicYearId + "\\" + "AssignementAnswers" + "\\" + CourseCycleId + "\\" + SectionId + "\\" + AssignemenetId + "\\" + AssignemenetAnswerId;    

        }

        public async Task<Result<string>> UploadFileForAssignementAnswer(UploadFileToAssignementAnswerDto uploadFileToAssignementAnswerDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId, int AssignemenetAnswerId)
        {
            try
            {
                string FolderPath = GetFileForAssignementAnswerPath(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignemenetId , AssignemenetAnswerId);
                if (!System.IO.File.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }
                string FileNameAndExtention = uploadFileToAssignementAnswerDto.Name + "." + uploadFileToAssignementAnswerDto.FileType.ToString().ToLower();

                string FilePath = FolderPath + "//" + FileNameAndExtention;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                using (FileStream stream = System.IO.File.Create(FilePath))
                {
                    await uploadFileToAssignementAnswerDto.FormFile.CopyToAsync(stream);
                    string RetuenedUrl = GetFileForAssignementAnswerPathInsidewwwroot(FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignemenetId, AssignemenetAnswerId) + "/" + FileNameAndExtention;
                    return Result.Success<string>(RetuenedUrl);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileForAssignementAnswer", "Exeption"));

            }
        }
    }
}
