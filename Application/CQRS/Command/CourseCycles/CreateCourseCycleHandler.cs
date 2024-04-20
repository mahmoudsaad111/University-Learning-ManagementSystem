﻿using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.CourseCycles;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.CourseCycles
{
    public class CreateCourseCycleHandler : ICommandHandler<CreateCourseCycleCommand, CourseCycle>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreateCourseCycleHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<CourseCycle>> Handle(CreateCourseCycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AcadimicYear acadimicYearHasGroup = await unitOfwork.GroupRepository.GetAcadimicYearHasSpecificGroup(request.CourseCycleDto.GroupId);
                AcadimicYear acadimicYearHasCourse=await unitOfwork.CourseRepository.GetAcadimicYearHasSpecificCourse(request.CourseCycleDto.CourseId);

                if(acadimicYearHasCourse is null || acadimicYearHasGroup is null || acadimicYearHasGroup.AcadimicYearId!= acadimicYearHasCourse.AcadimicYearId)
                    return Result.Failure<CourseCycle>(new Error(code: "Create CourseCycle", message: "not valid data"));

                CourseCycle courseCycle = await unitOfwork.CourseCycleRepository.CreateAsync(request.CourseCycleDto.GetCourseCycle());
                if (courseCycle is null)
                    return Result.Failure<CourseCycle>(new Error(code: "Create CourseCycle", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<CourseCycle>(courseCycle);
            }
            catch (Exception ex)
            {
                return Result.Failure<CourseCycle>(new Error(code: "Create CourseCycle", message: ex.Message.ToString()));
            }
        }
    }
}