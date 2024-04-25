using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Query.Instructors;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Instructors
{
    public class GetInstructorByIdHandler : IQueryHandler<GetInstructorByIdQuery , Instructor>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetInstructorByIdHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<Instructor>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Instructor instructor = await unitOfwork.InstructorRepository.GetByIdAsync(request.Id);
                if (instructor is null)
                    return Result.Failure<Instructor>(new Error(code: "GetInstructorById", message: "There is no Instructor By this Id"));
                return Result.Success<Instructor>(instructor);
            }
            catch (Exception ex)
            {
                return Result.Failure<Instructor>(new Error(code: "GetInstructor", message: ex.Message.ToString()));
            }
        }
    }
}
