using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.FileProcessing;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.FilesProcessing
{
    public class UploadFileToCourseHandler : ICommandHandler<UploadFileToCourseCommand, string>
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IFileCourseProcessing fileCourseProcessing;

        public UploadFileToCourseHandler(IUnitOfwork unitOfwork, IFileCourseProcessing fileCourseProcessing)
        {
            this.unitOfwork = unitOfwork;
            this.fileCourseProcessing = fileCourseProcessing;
        }

        public async Task<Result<string>> Handle(UploadFileToCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int CourseCycleId, AcadimicYearId, DepartementId, FacultyId;

                #region GetAllIdForRoute
                var CourseCycle = await unitOfwork.CourseCycleRepository.
                    GetCourseCycleUsingCourseIdAndGroupIdAsync(courseId: request.uploadFileToCourseDto.CourseId,
                                                                 groupId: request.uploadFileToCourseDto.GroupId);
                if (CourseCycle is null)
                    return Result.Failure<string>(new Error("", ""));

                CourseCycleId = CourseCycle.CourseCycleId;


                var AcadimicYear = await unitOfwork.AcadimicYearRepository
                    .GetAcadimicYearHasSpecificCourseIdAndGroupAsync(courseId: request.uploadFileToCourseDto.CourseId,
                                                                groupId: request.uploadFileToCourseDto.GroupId);

                if ( AcadimicYear is null)
                    return Result.Failure<string>(new Error("UplodingFileToCourse", "The Acadimic year or Course or Group contain this Section may be deleted"));

                AcadimicYearId=AcadimicYear.AcadimicYearId;

                var Departement= await unitOfwork.DepartementRepository.GetDepartementHasAcadimicYearId(AcadimicYearId);

                if(Departement is null)
                    return Result.Failure<string>(new Error("UploadFileToCourse", "The Departement contain this details of acadimic year and Course may be deleted"));

                DepartementId = Departement.DepartementId;
                FacultyId = Departement.FacultyId;
                #endregion


                Result<string> resultOfUploadingFile =await fileCourseProcessing.UploadFileForCourse(request.uploadFileToCourseDto,
                    FacultyId, DepartementId, AcadimicYearId, CourseCycleId, request.uploadFileToCourseDto.LectureId);

                if (resultOfUploadingFile is not null && resultOfUploadingFile.IsSuccess)
                {
                    return Result.Success<string>(resultOfUploadingFile.Value);
                }
                return Result.Failure<string>(new Error("UploadFileToCourse", "There is a probelm"));

            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileToCourse", "There is a probelm"));
            }
        }
    }
}
