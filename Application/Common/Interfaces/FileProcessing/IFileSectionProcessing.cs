using Contract.Dto.Files;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.FileProcessing
{
    public interface IFileSectionProcessing
    {
        public string GetFileForSectionPathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int LectureId);
        public string GetFileForSectionPath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int SectionId, int LectureId);
        public Task<Result<string>> UploadFileForSection(UploadFileToSectionDto uploadFileToCourseDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId,int SectionId, int LectureId);
    }
}
