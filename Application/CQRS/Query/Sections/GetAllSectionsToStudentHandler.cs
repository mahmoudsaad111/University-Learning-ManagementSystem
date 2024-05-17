using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Sections;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetAllSectionsToStudentHandler : IQueryHandler<GetAllSectionsToStudentQuery, IEnumerable<SectionOfStudentDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllSectionsToStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<SectionOfStudentDto>>> Handle(GetAllSectionsToStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student =await unitOfwork.UserRepository.GetUserByUserName(request.StudentUserName);
                if (student is null)
                    return Result.Failure<IEnumerable<SectionOfStudentDto>>(new Error(code: "GetAllSectionsToStudent", message: "In-valid data"));
                var SectionsOfStudent = await unitOfwork.StudentSectionRepository.GetAllSectionsOfStudent(StudentId: student.Id);

                return Result.Success(SectionsOfStudent);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<SectionOfStudentDto>>(new Error(code: "GetAllSectionsToStudent", message: ex.Message.ToString()));
            }
        }
    }
}
