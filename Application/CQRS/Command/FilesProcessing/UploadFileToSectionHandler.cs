using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.FileProcessing;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.FilesProcessing
{
    public class UploadFileToSectionHandler : ICommandHandler<UploadFileToSectionCommand, string>
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IFileSectionProcessing fileSectionProcessing;

        public UploadFileToSectionHandler(IUnitOfwork unitOfwork, IFileSectionProcessing fileSectionProcessing)
        {
            this.unitOfwork = unitOfwork;
            this.fileSectionProcessing = fileSectionProcessing;
        }

        public async Task<Result<string>> Handle(UploadFileToSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region GetAllIdForRoute
                int SectionId, CourseCycleId, AcadimicYearId, DepartementId, FacultyId;
                var Section = await unitOfwork.SectionRepository.GetSectionHasSpecificLectureUsingLectureIdAsync(request.uploadFileToSectionDto.LectureId) ;
                if (Section is null || Section.SectionId != request.uploadFileToSectionDto.SectionId)
                    return Result.Failure<string>(new Error("UploadFileToSection", "This LectureId is not belong to any Section"));

                SectionId = Section.SectionId;
                CourseCycleId = Section.CourseCycleId;
                var CourseCycle= await unitOfwork.CourseCycleRepository.FindAsync(c=>c.CourseCycleId==CourseCycleId);

                if (CourseCycle is null )
                    return Result.Failure<string>(new Error("UploadFileToSection", "The Course Cycle contain this SectionId id is deleted")) ;
                 

                var AcadimicYear =await unitOfwork.AcadimicYearRepository.GetAcadimicYearHasSpecificCourseIdAndGroupAsync(CourseCycle.CourseId, CourseCycle.GroupId);

                if(AcadimicYear is null )
                    return Result.Failure<string>(new Error("UploadFileToSection", "The Acadimic year or Course or Group contain this Section may be deleted"));

                AcadimicYearId = AcadimicYear.AcadimicYearId;
                DepartementId = AcadimicYear.DepartementId;

                var Departement=await unitOfwork.DepartementRepository.FindAsync(d=>d.DepartementId==DepartementId);

                if (Departement is null)
                    return Result.Failure<string>(new Error("UploadFileToSection", "The Departement contain this details of acadimic year and section may be deleted"));

                FacultyId = Departement.DepartementId;
                #endregion

                Result<string> resultOfUloadingFile = await fileSectionProcessing.UploadFileForSection(request.uploadFileToSectionDto,
                   FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, request.uploadFileToSectionDto.LectureId);

                if(resultOfUloadingFile is not null && resultOfUloadingFile.IsSuccess)
                {
                    return Result.Success<string>(resultOfUloadingFile.Value);
                }
                return Result.Failure<string>(new Error("UploadFileToSection", "There is a probelm"));
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileToSection", "There is a probelm"));
            }
        }
    }
}
