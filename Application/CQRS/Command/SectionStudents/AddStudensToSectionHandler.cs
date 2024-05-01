using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Models;
namespace Application.CQRS.Command.SectionStudents
{
    public class AddStudensToSectionHandler : ICommandHandler<AddStudensToSectionCommand, IEnumerable<string>>
    {
        private readonly IUnitOfwork unitOfwork;

        public AddStudensToSectionHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<string>>> Handle(AddStudensToSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Section =await unitOfwork.SectionRepository.GetByIdAsync(request.SectionId);
                if (Section == null)
                    return Result.Failure<IEnumerable<string>>(new Error(code: "AddStudensToSection", message: "Nullable"));

                List<string> StudentsWillNotAdded = new List<string> { };

                foreach(var studentUserName in request.StudentsUserNames)
                {
                    var StudentId =await unitOfwork.StudentRepository.GetStudentIdUsingUserName(studentUserName);
                    if (StudentId == 0)
                    {
                        StudentsWillNotAdded.Append(studentUserName);
                        continue; 
                    }

                    int StudentCourseCycleId = await unitOfwork.StudentCourseCycleRepository.GetStudentCourseCycleId(studentId: StudentId, courseCycleId: Section.CourseCycleId);

                    if(StudentCourseCycleId == 0)
                    {
                        StudentsWillNotAdded.Append(studentUserName);
                        continue;
                    }

                    var AddedStudentSection = await unitOfwork.StudentSectionRepository.CreateAsync(new StudentSection
                    {
                        SectionId = Section.SectionId,
                        StudentCourseCycleId = StudentCourseCycleId
                    });

                    if (AddedStudentSection is null)
                        StudentsWillNotAdded.Append(studentUserName);
                }

                if (StudentsWillNotAdded.Count == 0)
                {
                    await unitOfwork.SaveChangesAsync();
                    return Result.Success<IEnumerable<string>>(StudentsWillNotAdded);
                }
                else if (StudentsWillNotAdded.Count > 0 && StudentsWillNotAdded.Count <= request.StudentsUserNames.Count()) 
                {
                    // return the Invalid UserNames and handle in api with return BadRequest(Inavlid userNames array)
                    return Result.Success<IEnumerable<string>>(StudentsWillNotAdded);
                }
                return Result.Failure<IEnumerable<string>>(new Error(code: "Add students to section", message: "Can not add students to section"));
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error(code: "AddStudensToSection", message: ex.ToString()));
            }
        }
    }
}
