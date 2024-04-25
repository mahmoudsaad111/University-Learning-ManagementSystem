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

                IEnumerable<string> StudentsWillNotAdded = new List<string> { };

                foreach(var studentUserName in request.StudentsUserNames)
                {
                    var StudentId =await unitOfwork.StudentRepository.GetStudentIdUsingUserName(studentUserName); 
                    if(StudentId == 0)
                    {
                        StudentsWillNotAdded.Append(studentUserName);
                        continue;
                    }

                    var StudentCourseCycleId = await unitOfwork.StudentCourseCycleRepository.GetStudentCourseCycleId(studentId: StudentId, courseCycleId: Section.CourseCycleId);

                    var AddedStudentSection = await unitOfwork.StudentSectionRepository.CreateAsync(new StudentSection
                    {
                        SectionId = Section.SectionId,
                        StudentCourseCycleId = StudentCourseCycleId
                    });

                    if (AddedStudentSection is null)
                        StudentsWillNotAdded.Append(studentUserName);
                }
                await unitOfwork.SaveChangesAsync();
                return Result.Success<IEnumerable<string>>(StudentsWillNotAdded);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error(code: "AddStudensToSection", message: ex.ToString()));
            }
        }
    }
}
