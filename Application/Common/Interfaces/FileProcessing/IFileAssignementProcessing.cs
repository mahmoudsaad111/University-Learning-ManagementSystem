using Contract.Dto.Files;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.FileProcessing
{
    public interface IFileAssignementProcessing
    {
        public string GetFileForAssignementPathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId , int AssignemenetId);
        public string GetFileForAssignementPath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId);
        public Task<Result<string>> UploadFileForAssignement(UploadFileToAssignementDto uploadFileToAssignementDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId);
    }
}
