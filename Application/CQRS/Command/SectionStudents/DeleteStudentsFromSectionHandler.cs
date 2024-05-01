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

                List<string> StudentsWillNotDelted = new List<string> { };

                foreach (var studentUserName in request.StudentsUserNames)
                {
                    var StudentId = await unitOfwork.StudentRepository.GetStudentIdUsingUserName(studentUserName);
                    if (StudentId == 0)
                    {
                        StudentsWillNotDelted.Append(studentUserName);
                        continue;
                    }

                    var StudentCourseCycleId = await unitOfwork.StudentCourseCycleRepository.GetStudentCourseCycleId(studentId: StudentId, courseCycleId: Section.CourseCycleId);
                    if(StudentCourseCycleId == 0)
                    {
                        StudentsWillNotDelted.Add(studentUserName);
                        continue;
                    }
                    var StudentSectionId = await unitOfwork.StudentSectionRepository.GetStudentSectionId(SectionId: Section.SectionId, StudentCourseCycleId: StudentCourseCycleId);

                    if (StudentSectionId == 0)
                        StudentsWillNotDelted.Append(studentUserName);
                    else
                    {
                        bool Deleted = await unitOfwork.StudentSectionRepository.DeleteAsync(StudentSectionId);

                        if (!Deleted)
                            StudentsWillNotDelted.Append(studentUserName);
                    }
                }

                if (StudentsWillNotDelted.Count == 0)
                {
                    await unitOfwork.SaveChangesAsync();
                    return Result.Success<IEnumerable<string>>(StudentsWillNotDelted);
                }
                else if (StudentsWillNotDelted.Count > 0 && StudentsWillNotDelted.Count <= request.StudentsUserNames.Count())
                {
                    // return the Invalid UserNames and handle in api with return BadRequest(Inavlid userNames array)
                    return Result.Success<IEnumerable<string>>(StudentsWillNotDelted);
                }
                return Result.Failure<IEnumerable<string>>(new Error(code: "Delete students to section", message: "Can not delete students to section"));

            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error(code: "AddStudensToSection", message: ex.ToString()));
            }
        }
    }
}
