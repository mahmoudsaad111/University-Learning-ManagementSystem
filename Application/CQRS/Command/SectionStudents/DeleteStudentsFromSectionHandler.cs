using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.SectionStudents
{
    public class DeleteStudentsFromSectionHandler : ICommandHandler<DeleteStudentsFromSectionCommand, IEnumerable<string>>
    {
        private readonly IUnitOfwork unitOfwork;

        public DeleteStudentsFromSectionHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<string>>> Handle(DeleteStudentsFromSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Section = await unitOfwork.SectionRepository.GetByIdAsync(request.SectionId);
                if (Section == null)
                    return Result.Failure<IEnumerable<string>>(new Error(code: "AddStudensToSection", message: "Nullable"));

                IEnumerable<string> StudentsWillNotDelted = new List<string> { };

                foreach (var studentUserName in request.StudentsUserNames)
                {
                    var StudentId = await unitOfwork.StudentRepository.GetStudentIdUsingUserName(studentUserName);
                    if (StudentId == 0)
                    {
                        StudentsWillNotDelted.Append(studentUserName);
                        continue;
                    }

                    var StudentCourseCycleId = await unitOfwork.StudentCourseCycleRepository.GetStudentCourseCycleId(studentId: StudentId, courseCycleId: Section.CourseCycleId);

                    var StudentSectionId = await unitOfwork.StudentSectionRepository.GetStudentSectionId(SectionId: Section.SectionId, StudentCourseCycleId: StudentCourseCycleId);

                    if (StudentSectionId == 0)
                        StudentsWillNotDelted.Append(studentUserName);
                    else
                    {
                        await unitOfwork.StudentSectionRepository.DeleteAsync(StudentSectionId);
                    }
                }

                await unitOfwork.SaveChangesAsync();
                return Result.Success<IEnumerable<string>>(StudentsWillNotDelted);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error(code: "AddStudensToSection", message: ex.ToString()));
            }
        }
    }
}
