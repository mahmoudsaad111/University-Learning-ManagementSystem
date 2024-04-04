using Contract.Dto.Files;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.FileProcessing
{
    public interface IFileAssignementAnswerProcessing
    {
        public string GetFileForAssignementAnswerPathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId, int AssignementAnswerId);
        public string GetFileForAssignementAnswerPath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId, int AssignementAnswerId);
        public Task<Result<string>> UploadFileForAssignementAnswer(UploadFileToAssignementAnswerDto uploadFileToAssignementDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int AssignemenetId, int AssignementAnswerId);
    }
}
