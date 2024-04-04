using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Sections;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Sections
{
    public class CreateSectionHandler : ICommandHandler<CreateSectionCommand, Section>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreateSectionHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<Section>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Section section = await unitOfwork.SectionRepository.CreateAsync(request.SectionDto.GetSection());
                if (section is null)
                    return Result.Failure<Section>(new Error(code: "Create Section", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<Section>(section);
            }
            catch (Exception ex)
            {
                return Result.Failure<Section>(new Error(code: "Create Section", message: ex.Message.ToString()));
            }
        }
    }
}
