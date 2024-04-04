using Contract.Dto.Files;
using Domain.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.FileProcessing
{
    public interface IFileCourseProcessing
    {
        public string GetFileForCoursePathInsidewwwroot(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int LectureId);
        public string GetFileForCoursePath(int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int LectureId);
        public Task<Result<string>> UploadFileForCourse(UploadFileToCourseDto uploadFileToCourseDto, int FacultyId, int DepartementId, int AcadimicYearId, int CourseCycleId, int LectureId);
    }
}
