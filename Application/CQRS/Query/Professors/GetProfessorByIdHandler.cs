using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Query.Professors;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Professors
{
    public class GetProfessorByIdHandler : IQueryHandler<GetProfessorByIdQuery , Professor>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetProfessorByIdHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<Professor>> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Professor professor = await unitOfwork.ProfessorRepository.GetByIdAsync(request.Id);
                if (professor is null)
                    return Result.Failure<Professor>(new Error(code: "GetProfessorById", message: "There is no Professor By this Id"));
                return Result.Success<Professor>(professor);
            }
            catch (Exception ex)
            {
                return Result.Failure<Professor>(new Error(code: "GetProfessor", message: ex.Message.ToString()));
            }
        }
    }
}
